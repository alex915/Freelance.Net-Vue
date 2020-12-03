import Axios from 'axios';

class Companies {
 
 public createCompany(company : any) {
     return Axios.post('./api/Companies',company);
 }

}

export const companiesService = new Companies();