import Axios, { AxiosResponse } from 'axios';

class Companies {
 public createCompany(company: any) {
     return Axios.post('./api/Companies',company);
 }
 
 public updateCompany(company: any) {
     return Axios.put('./api/Companies/'+company.id,company);
 }
 
 public getCompanies() {
     return Axios.get('./api/Companies/all');
 }

 public deleteCompany(companyId: number) {
     return Axios.delete(`./api/Companies/${companyId}`);
 }


}

export const companiesService = new Companies();