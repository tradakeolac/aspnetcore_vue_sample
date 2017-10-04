import axios from 'axios'

axios.interceptors.request.use((config) => {
    const tokenStorage = localStorage.getItem("__TOKEN__");

    if(tokenStorage) {
        let token = JSON.parse(tokenStorage);
        if(token) {
            config.headers.Authorization = `${token.token_type} ${token.access_token}`;
        }
    }

    return config;
}, (error) => {
    return Promise.reject(error);
})

export const http = axios.create({
    baseURL: '/api/v1.0',
})

