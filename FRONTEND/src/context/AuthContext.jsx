import React, { createContext, useContext, useState, useEffect } from 'react';
import api from '../api/axios';

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const token = localStorage.getItem('token');
        const role = localStorage.getItem('role');
        const username = localStorage.getItem('username');
        if (token && role) setUser({ token, role, username });
        setLoading(false);
    }, []);

    const login = async (username, password) => {
        try {
            const { data } = await api.post('/auth/login', { username, password });
            localStorage.setItem('token', data.token);
            localStorage.setItem('role', data.role);
            localStorage.setItem('username', username);
            setUser({ token: data.token, role: data.role, username });
            return { success: true };
        } catch (err) {
            const msg = err.response?.data;
            return { success: false, message: typeof msg === 'string' ? msg : msg?.title || 'Invalid credentials' };
        }
    };

    const logout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('role');
        localStorage.removeItem('username');
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, login, logout, loading }}>
            {!loading && children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);
