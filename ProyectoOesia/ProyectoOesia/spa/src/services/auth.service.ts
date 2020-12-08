import Axios from 'axios';

class AuthService {
    
    //llama a la api mediante axios y devuelve la promesa
    public login(email: string, password: string) {
        return Axios.get(`./api/auth?email=${email}&password=${password}`);
    }

    public register(user: object) {
        return Axios.post(`./api/auth`,user);
    }
    public update(user: any) {
        const config = {
            headers: {
                'content-type': 'multipart/form-data',
              },
          };
          const formData = new FormData();
          for (const key in user) {
            if (Object.prototype.hasOwnProperty.call(user, key)) {
              const element = user[key];
              formData.append(key, element);
            }
          }
          return Axios.put(`./api/auth`, formData, config); 
    }
   
    public getUser() {
        return Axios.get(`./api/auth/user`);
    }
}

export const authService = new AuthService();