import Axios from 'axios';

class Contacts {
 
 public getContacts(id: any) {
     return Axios.get('./api/Contacts/'+id);
 }
 public contact(id: any) {
     return Axios.post('./api/Contacts/',id);
 }
 public viewContact(id: any) {
     return Axios.put('./api/Contacts/'+id);
 }

}

export const contactsService = new Contacts();