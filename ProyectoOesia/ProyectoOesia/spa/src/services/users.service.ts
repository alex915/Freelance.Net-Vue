import Axios from 'axios';

class UsersService {
 public get() {
     return Axios.get('./api/Users');
 }

 public delete(userId: string) {
     return Axios.delete(`./api/Users/${userId}`);
 }
}

export const usersService = new UsersService();