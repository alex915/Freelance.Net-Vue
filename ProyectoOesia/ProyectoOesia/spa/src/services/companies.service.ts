import Axios from 'axios';

class Companies {
 
 public createCompany(company: any, token: any) {
     return Axios.post('./api/Companies',company,{
        headers: {
          'Authorization': `Bearer ${token}` 
        }
      });
 }

}

export const companiesService = new Companies();