import axios from 'axios';
import store from '@/store';

export const TokenInterceptor = () => {
    axios.interceptors.request.use((config: any) => {
        const authToken = store.getters['token'];
        config.headers['Content-Type'] = 'application/json';
        
        if (authToken && config.url.includes("/api/")) {
          config.headers.Authorization = `Bearer ${authToken}`;
        }
        return config;
    }, (err: any) => {
        console.log(err);
        return Promise.reject(err);
    });
};