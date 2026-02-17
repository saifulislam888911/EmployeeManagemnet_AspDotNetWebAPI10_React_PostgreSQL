import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import api from '../api/axios';
import { ArrowLeft, Save, Plus, Trash2, User, Phone, Briefcase, CreditCard, Heart, Baby } from 'lucide-react';

const DEPARTMENTS = ['Engineering', 'Human Resources', 'Finance', 'Marketing', 'Operations', 'IT Support'];

const EmployeeForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const isEdit = !!id;
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');
    const [formData, setFormData] = useState({
        name: '',
        nid: '',
        phone: '',
        department: '',
        basicSalary: '',
        spouse: null,
        children: []
    });
    const [hasSpouse, setHasSpouse] = useState(false);

    useEffect(() => {
        if (isEdit) fetchEmployee();
    }, [id]);

    const fetchEmployee = async () => {
        setLoading(true);
        try {
            const response = await api.get(`/employees/${id}`);
            const data = response.data;
            setFormData({
                ...data,
                spouse: data.spouse,
                children: data.children || []
            });
            setHasSpouse(!!data.spouse);
        } catch (err) {
            setError('Failed to fetch employee');
        } finally {
            setLoading(false);
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleSpouseChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, spouse: { ...prev.spouse, [name]: value } }));
    };

    const toggleSpouse = (checked) => {
        setHasSpouse(checked);
        setFormData(prev => ({ ...prev, spouse: checked ? { name: '', nid: '' } : null }));
    };

    const addChild = () => {
        setFormData(prev => ({ ...prev, children: [...prev.children, { name: '', dateOfBirth: '' }] }));
    };

    const removeChild = (index) => {
        setFormData(prev => ({ ...prev, children: prev.children.filter((_, i) => i !== index) }));
    };

    const handleChildChange = (index, field, value) => {
        const newChildren = [...formData.children];
        newChildren[index][field] = value;
        setFormData(prev => ({ ...prev, children: newChildren }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError('');
        try {
            if (isEdit) await api.put(`/employees/${id}`, formData);
            else await api.post('/employees', formData);
            navigate('/dashboard');
        } catch (err) {
            const msg = err.response?.data;
            setError(typeof msg === 'string' ? msg : msg?.title || msg?.Message || 'Failed to save');
        } finally {
            setLoading(false);
        }
    };

    if (loading && isEdit && !formData.name) {
        return <div className="text-center text-slate-500 py-12">Loading...</div>;
    }

    return (
        <div className="max-w-4xl mx-auto space-y-6">
            <div className="flex items-center gap-4">
                <button onClick={() => navigate('/dashboard')} className="p-2 bg-slate-800 rounded-lg text-slate-400 hover:text-white transition-colors">
                    <ArrowLeft size={20} />
                </button>
                <div>
                    <h1 className="text-2xl font-bold text-white">{isEdit ? 'Edit Employee' : 'New Employee'}</h1>
                    <p className="text-slate-400 text-sm">{isEdit ? 'Update employee information' : 'Add a new employee'}</p>
                </div>
            </div>

            {error && (
                <div className="p-4 bg-red-500/10 border border-red-500/20 rounded-xl text-red-400">{error}</div>
            )}

            <form onSubmit={handleSubmit} className="space-y-8">
                <div className="bg-slate-900 border border-slate-800 rounded-xl p-6 space-y-6">
                    <h3 className="text-lg font-semibold text-white flex items-center gap-2">
                        <User size={20} className="text-blue-400" /> Basic Information
                    </h3>
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                        <div className="space-y-2">
                            <label className="text-sm font-medium text-slate-300">Full Name</label>
                            <input type="text" name="name" value={formData.name} onChange={handleChange}
                                className="w-full bg-slate-800 border border-slate-700 rounded-lg px-4 py-2.5 text-white focus:ring-2 focus:ring-blue-500/50 focus:outline-none" required />
                        </div>
                        <div className="space-y-2">
                            <label className="text-sm font-medium text-slate-300">National ID (NID)</label>
                            <input type="text" name="nid" value={formData.nid} onChange={handleChange}
                                className="w-full bg-slate-800 border border-slate-700 rounded-lg px-4 py-2.5 text-white focus:ring-2 focus:ring-blue-500/50 focus:outline-none" required />
                        </div>
                        <div className="space-y-2">
                            <label className="text-sm font-medium text-slate-300 flex items-center gap-2"><Phone size={14} /> Phone</label>
                            <input type="text" name="phone" value={formData.phone} onChange={handleChange}
                                className="w-full bg-slate-800 border border-slate-700 rounded-lg px-4 py-2.5 text-white focus:ring-2 focus:ring-blue-500/50 focus:outline-none" required />
                        </div>
                        <div className="space-y-2">
                            <label className="text-sm font-medium text-slate-300 flex items-center gap-2"><Briefcase size={14} /> Department</label>
                            <select name="department" value={formData.department} onChange={handleChange}
                                className="w-full bg-slate-800 border border-slate-700 rounded-lg px-4 py-2.5 text-white focus:ring-2 focus:ring-blue-500/50 focus:outline-none" required>
                                <option value="">Select Department</option>
                                {DEPARTMENTS.map(d => <option key={d} value={d}>{d}</option>)}
                            </select>
                        </div>
                        <div className="space-y-2">
                            <label className="text-sm font-medium text-slate-300 flex items-center gap-2"><CreditCard size={14} /> Basic Salary</label>
                            <input type="number" name="basicSalary" value={formData.basicSalary} onChange={handleChange}
                                className="w-full bg-slate-800 border border-slate-700 rounded-lg px-4 py-2.5 text-white focus:ring-2 focus:ring-blue-500/50 focus:outline-none" required />
                        </div>
                    </div>
                </div>

                <div className="bg-slate-900 border border-slate-800 rounded-xl p-6 space-y-6">
                    <div className="flex items-center justify-between">
                        <h3 className="text-lg font-semibold text-white flex items-center gap-2">
                            <Heart size={20} className="text-rose-400" /> Spouse
                        </h3>
                        <label className="relative inline-flex items-center cursor-pointer">
                            <input type="checkbox" checked={hasSpouse} onChange={(e) => toggleSpouse(e.target.checked)} className="sr-only peer" />
                            <div className="w-11 h-6 bg-slate-700 rounded-full peer peer-checked:bg-blue-600 after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:after:translate-x-full"></div>
                            <span className="ml-3 text-sm text-slate-300">Has Spouse</span>
                        </label>
                    </div>
                    {hasSpouse && (
                        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                            <div className="space-y-2">
                                <label className="text-sm font-medium text-slate-300">Spouse Name</label>
                                <input type="text" name="name" value={formData.spouse?.name || ''} onChange={handleSpouseChange}
                                    className="w-full bg-slate-800 border border-slate-700 rounded-lg px-4 py-2.5 text-white focus:ring-2 focus:ring-blue-500/50 focus:outline-none" required />
                            </div>
                            <div className="space-y-2">
                                <label className="text-sm font-medium text-slate-300">Spouse NID</label>
                                <input type="text" name="nid" value={formData.spouse?.nid || ''} onChange={handleSpouseChange}
                                    className="w-full bg-slate-800 border border-slate-700 rounded-lg px-4 py-2.5 text-white focus:ring-2 focus:ring-blue-500/50 focus:outline-none" required />
                            </div>
                        </div>
                    )}
                </div>

                <div className="bg-slate-900 border border-slate-800 rounded-xl p-6 space-y-6">
                    <div className="flex items-center justify-between">
                        <h3 className="text-lg font-semibold text-white flex items-center gap-2">
                            <Baby size={20} className="text-amber-400" /> Children
                        </h3>
                        <button type="button" onClick={addChild} className="flex items-center gap-2 px-3 py-1.5 bg-slate-800 hover:bg-slate-700 text-blue-400 rounded-lg text-sm">
                            <Plus size={16} /> Add Child
                        </button>
                    </div>
                    {formData.children.length === 0 ? (
                        <div className="text-center py-8 text-slate-500 text-sm border-2 border-dashed border-slate-800 rounded-lg">No children. Click Add Child if needed.</div>
                    ) : (
                        <div className="space-y-4">
                            {formData.children.map((child, index) => (
                                <div key={index} className="flex flex-col md:flex-row gap-4 p-4 bg-slate-800/50 rounded-xl">
                                    <div className="flex-1 space-y-2">
                                        <label className="text-xs font-medium text-slate-400">Name</label>
                                        <input type="text" value={child.name} onChange={(e) => handleChildChange(index, 'name', e.target.value)}
                                            className="w-full bg-slate-900 border border-slate-700 rounded-lg px-3 py-2 text-white text-sm focus:ring-1 focus:ring-blue-500/50 focus:outline-none" placeholder="Child Name" required />
                                    </div>
                                    <div className="flex-1 space-y-2">
                                        <label className="text-xs font-medium text-slate-400">Date of Birth</label>
                                        <input type="date" value={child.dateOfBirth ? child.dateOfBirth.split('T')[0] : ''} onChange={(e) => handleChildChange(index, 'dateOfBirth', e.target.value)}
                                            className="w-full bg-slate-900 border border-slate-700 rounded-lg px-3 py-2 text-white text-sm focus:ring-1 focus:ring-blue-500/50 focus:outline-none" required />
                                    </div>
                                    <button type="button" onClick={() => removeChild(index)} className="md:self-end p-2 text-slate-500 hover:text-red-400 hover:bg-slate-700 rounded-lg">
                                        <Trash2 size={18} />
                                    </button>
                                </div>
                            ))}
                        </div>
                    )}
                </div>

                <div className="flex justify-end pt-4">
                    <button type="submit" disabled={loading}
                        className="flex items-center gap-2 px-8 py-3 bg-blue-600 hover:bg-blue-500 text-white font-semibold rounded-xl disabled:opacity-50 disabled:cursor-not-allowed">
                        <Save size={20} />
                        <span>{loading ? 'Saving...' : 'Save'}</span>
                    </button>
                </div>
            </form>
        </div>
    );
};

export default EmployeeForm;
