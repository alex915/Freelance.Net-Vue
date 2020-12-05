import Axios from 'axios';

class Companies {
 
 public createCompany(company: any) {
     return Axios.post('./api/Companies',company);
 }
 public updateCompany(company: any) {
     return Axios.put('./api/Companies/'+company.id,company);
 }


}

export const companiesService = new Companies();