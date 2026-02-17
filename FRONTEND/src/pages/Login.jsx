import React, { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const { login } = useAuth();
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        const result = await login(username, password);
        if (result.success) navigate('/dashboard');
        else setError(result.message || 'Invalid credentials');
    };

    return (
        <div className="min-h-screen bg-slate-950 flex items-center justify-center p-4">
            <div className="w-full max-w-md bg-slate-900 border border-slate-800 rounded-xl p-8">
                <h1 className="text-2xl font-bold text-white mb-2 text-center">Employee Management</h1>
                <p className="text-slate-400 text-center mb-6">Sign in to continue</p>

                {error && <div className="mb-4 p-3 bg-red-500/10 border border-red-500/20 rounded-lg text-red-400 text-sm text-center">{error}</div>}

                <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                        <label className="block text-sm text-slate-300 mb-1">Username</label>
                        <input type="text" value={username} onChange={(e) => setUsername(e.target.value)}
                            className="w-full bg-slate-800 border border-slate-700 rounded-lg py-2.5 px-4 text-white focus:ring-2 focus:ring-blue-500/50 focus:outline-none" placeholder="Username" required />
                    </div>
                    <div>
                        <label className="block text-sm text-slate-300 mb-1">Password</label>
                        <input type="password" value={password} onChange={(e) => setPassword(e.target.value)}
                            className="w-full bg-slate-800 border border-slate-700 rounded-lg py-2.5 px-4 text-white focus:ring-2 focus:ring-blue-500/50 focus:outline-none" placeholder="Password" required />
                    </div>
                    <button type="submit" className="w-full bg-blue-600 hover:bg-blue-500 text-white font-semibold py-3 rounded-lg">
                        Sign In
                    </button>
                </form>

                <p className="mt-6 text-xs text-slate-500 text-center">Admin: admin / admin123 | Viewer: viewer / viewer123</p>
            </div>
        </div>
    );
};

export default Login;
