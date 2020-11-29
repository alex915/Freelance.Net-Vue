import Axios from 'axios';

class AuthService {
    //llama a la api mediante axios y devuelve la promesa
    public login(email: string, password: string) {
        return Axios.get(`./api/auth?email=${email}&password=${password}`);
    }
    public register(email: string, password: string) {
        return Axios.post(`./api/auth/signup`, {
            email,
            password,
        });
    }
}

export const authService = new AuthService();