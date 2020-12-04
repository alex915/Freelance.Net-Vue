import axios from 'axios';
import store from '@/store';

export const TokenInterceptor = () => {
    axios.interceptors.request.use((config: any) => {
        const authToken = store.getters['getToken'];
        config.headers['Content-Type'] = 'application/json';

        if (authToken) {
            config.headers.Authorization = `Bearer ${authToken}`;
        }
        return config;
    }, (err: any) => {
        return Promise.reject(err);
    });
};