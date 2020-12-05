import Axios from 'axios';

class AuthService {
    
    //llama a la api mediante axios y devuelve la promesa
    public search(activity: string) {
        return Axios.get(`./api/auth?activity=${activity}`);
    }

    public filter(company: string, activity: string) {
        return Axios.post(`./api/auth?company=${company}&&.activity=${activity}`, {
            company,
            activity,
        });
    }
}

export const authService = new AuthService();