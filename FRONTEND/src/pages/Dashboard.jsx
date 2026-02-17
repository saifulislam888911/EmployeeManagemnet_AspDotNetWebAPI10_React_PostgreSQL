import React, { useState, useEffect } from 'react';
import api from '../api/axios';
import { Search, Plus, FileDown, Edit, Trash2, Users } from 'lucide-react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { API_BASE_URL } from '../config';

const DEBOUNCE_MS = 400;

const Dashboard = () => {
    const [employees, setEmployees] = useState([]);
    const [loading, setLoading] = useState(true);
    const [search, setSearch] = useState('');
    const [debouncedSearch, setDebouncedSearch] = useState('');
    const { user } = useAuth();

    useEffect(() => {
        const timer = setTimeout(() => setDebouncedSearch(search), DEBOUNCE_MS);
        return () => clearTimeout(timer);
    }, [search]);

    useEffect(() => {
        fetchEmployees(debouncedSearch);
    }, [debouncedSearch]);

    const fetchEmployees = async (query = '') => {
        setLoading(true);
        try {
            const endpoint = query ? `/employees/search?q=${encodeURIComponent(query)}` : '/employees';
            const response = await api.get(endpoint);
            setEmployees(response.data);
        } catch (error) {
            console.error("Failed to fetch employees:", error);
        } finally {
            setLoading(false);
        }
    };

    const handleSearch = (e) => setSearch(e.target.value);

    const deleteEmployee = async (id) => {
        if (!window.confirm('Are you sure you want to delete this employee?')) return;
        try {
            await api.delete(`/employees/${id}`);
            fetchEmployees(debouncedSearch);
        } catch (error) {
            alert('Failed to delete employee');
        }
    };

    const exportPdf = () => {
        const url = debouncedSearch
            ? `${API_BASE_URL}/employees/export/pdf?q=${encodeURIComponent(debouncedSearch)}`
            : `${API_BASE_URL}/employees/export/pdf`;
        window.open(url, '_blank');
    };

    return (
        <div className="space-y-6">
            <div className="flex flex-col md:flex-row md:items-center justify-between gap-4">
                <div>
                    <h2 className="text-2xl font-bold text-white mb-1">Employee Directory</h2>
                    <p className="text-slate-400 text-sm">Manage and track your team</p>
                </div>
                <div className="flex items-center gap-3">
                    <button
                        onClick={exportPdf}
                        className="flex items-center gap-2 px-4 py-2 bg-slate-800 hover:bg-slate-700 text-slate-200 rounded-lg transition-colors border border-slate-700"
                    >
                        <FileDown size={18} />
                        <span>Export PDF</span>
                    </button>
                    {user?.role === 'Admin' && (
                        <Link
                            to="/employee/new"
                            className="flex items-center gap-2 px-4 py-2 bg-blue-600 hover:bg-blue-500 text-white rounded-lg transition-colors"
                        >
                            <Plus size={18} />
                            <span>Add Employee</span>
                        </Link>
                    )}
                </div>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
                <div className="md:col-span-3 bg-slate-900 border border-slate-800 rounded-xl p-4 flex items-center">
                    <Search className="text-slate-500 ml-2" size={20} />
                    <input
                        type="text"
                        placeholder="Search by name, NID, or department..."
                        value={search}
                        onChange={handleSearch}
                        className="bg-transparent border-none focus:ring-0 text-white flex-1 ml-3 placeholder:text-slate-600"
                    />
                </div>
                <div className="bg-slate-900 border border-slate-800 rounded-xl p-4 flex items-center justify-between">
                    <div>
                        <p className="text-slate-400 text-xs font-medium uppercase">Total Staff</p>
                        <p className="text-2xl font-bold text-white">{employees.length}</p>
                    </div>
                    <div className="w-10 h-10 bg-blue-500/20 rounded-lg flex items-center justify-center text-blue-400">
                        <Users size={20} />
                    </div>
                </div>
            </div>

            <div className="bg-slate-900 border border-slate-800 rounded-xl overflow-hidden">
                <div className="overflow-x-auto">
                    <table className="w-full text-left">
                        <thead>
                            <tr className="bg-slate-800/50 text-slate-400 text-sm uppercase">
                                <th className="px-6 py-4 font-medium">Employee</th>
                                <th className="px-6 py-4 font-medium">Contact</th>
                                <th className="px-6 py-4 font-medium">Department</th>
                                <th className="px-6 py-4 font-medium text-right">Actions</th>
                            </tr>
                        </thead>
                        <tbody className="divide-y divide-slate-800">
                            {loading ? (
                                <tr>
                                    <td colSpan="4" className="px-6 py-12 text-center text-slate-500">Loading...</td>
                                </tr>
                            ) : employees.length === 0 ? (
                                <tr>
                                    <td colSpan="4" className="px-6 py-12 text-center text-slate-500">No employees found.</td>
                                </tr>
                            ) : (
                                employees.map((emp) => (
                                    <tr key={emp.id} className="hover:bg-slate-800/30 transition-colors">
                                        <td className="px-6 py-4">
                                            <div className="flex items-center gap-3">
                                                <div className="w-10 h-10 rounded-full bg-slate-700 flex items-center justify-center text-slate-300 font-bold">
                                                    {emp.name?.charAt(0) || '?'}
                                                </div>
                                                <div>
                                                    <p className="font-medium text-white">{emp.name}</p>
                                                    <p className="text-xs text-slate-500">NID: {emp.nid}</p>
                                                </div>
                                            </div>
                                        </td>
                                        <td className="px-6 py-4 text-slate-300">{emp.phone}</td>
                                        <td className="px-6 py-4">
                                            <span className="px-3 py-1 rounded-full text-xs font-medium bg-blue-500/10 text-blue-400 border border-blue-500/10">
                                                {emp.department}
                                            </span>
                                        </td>
                                        <td className="px-6 py-4 text-right">
                                            <div className="flex items-center justify-end gap-2">
                                                <button
                                                    onClick={() => window.open(`${API_BASE_URL}/employees/${emp.id}/cv/pdf`, '_blank')}
                                                    className="p-2 text-slate-400 hover:text-blue-400 hover:bg-slate-800 rounded-lg transition-colors"
                                                    title="Download CV"
                                                >
                                                    <FileDown size={18} />
                                                </button>
                                                {user?.role === 'Admin' && (
                                                    <>
                                                        <Link
                                                            to={`/employee/edit/${emp.id}`}
                                                            className="p-2 text-slate-400 hover:text-amber-400 hover:bg-slate-800 rounded-lg transition-colors"
                                                            title="Edit"
                                                        >
                                                            <Edit size={18} />
                                                        </Link>
                                                        <button
                                                            onClick={() => deleteEmployee(emp.id)}
                                                            className="p-2 text-slate-400 hover:text-red-400 hover:bg-slate-800 rounded-lg transition-colors"
                                                            title="Delete"
                                                        >
                                                            <Trash2 size={18} />
                                                        </button>
                                                    </>
                                                )}
                                            </div>
                                        </td>
                                    </tr>
                                ))
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
};

export default Dashboard;
