import React from 'react';
import { Outlet, Link, useNavigate, useLocation } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { LayoutDashboard, UserPlus, LogOut, User } from 'lucide-react';

const Layout = () => {
    const { user, logout } = useAuth();
    const navigate = useNavigate();
    const location = useLocation();

    const NavItem = ({ to, icon: Icon, label }) => {
        const isActive = location.pathname === to;
        return (
            <Link to={to} className={`flex items-center gap-3 px-4 py-3 rounded-xl transition-all ${isActive ? 'bg-blue-600 text-white' : 'text-slate-400 hover:bg-slate-800 hover:text-white'}`}>
                <Icon size={20} />
                <span className="font-medium">{label}</span>
            </Link>
        );
    };

    return (
        <div className="flex h-screen bg-slate-950 text-slate-200">
            <aside className="w-64 bg-slate-900 border-r border-slate-800 flex flex-col">
                <div className="p-6 border-b border-slate-800">
                    <h1 className="text-xl font-bold text-blue-400">Employee Management</h1>
                </div>
                <nav className="flex-1 p-4 space-y-2">
                    <NavItem to="/dashboard" icon={LayoutDashboard} label="Dashboard" />
                    {user?.role === 'Admin' && <NavItem to="/employee/new" icon={UserPlus} label="Add Employee" />}
                </nav>
                <div className="p-4 border-t border-slate-800">
                    <div className="flex items-center gap-3 px-4 py-3 mb-2">
                        <div className="w-8 h-8 rounded-full bg-blue-500/20 flex items-center justify-center text-blue-400">
                            <User size={16} />
                        </div>
                        <div className="flex-1 min-w-0">
                            <p className="text-sm font-medium text-white truncate">{user?.username}</p>
                            <p className="text-xs text-slate-500">{user?.role}</p>
                        </div>
                    </div>
                    <button onClick={() => { logout(); navigate('/login'); }} className="w-full flex items-center gap-3 px-4 py-2 text-red-400 hover:bg-red-500/10 rounded-lg text-sm">
                        <LogOut size={16} /> Sign Out
                    </button>
                </div>
            </aside>
            <main className="flex-1 overflow-auto bg-slate-950">
                <div className="p-8 max-w-7xl mx-auto">
                    <Outlet />
                </div>
            </main>
        </div>
    );
};

export default Layout;
