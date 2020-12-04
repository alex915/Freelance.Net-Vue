import Axios from 'axios';

class AuthService {
    
    //llama a la api mediante axios y devuelve la promesa
    public login(email: string, password: string) {
        return Axios.get(`./api/auth?email=${email}&password=${password}`);
    }

    public register(user: object) {
        return Axios.post(`./api/auth`,user);
    }
    public update(user: object) {
        return Axios.put(`./api/auth`,user);
    }
   
    public getUser() {
        return Axios.get(`./api/auth/user`);
    }
}

export const authService = new AuthService();