import Axios, { AxiosResponse } from 'axios';

class Companies {
 public createCompany(company: any) {
    const config = {
        headers: {
            'content-type': 'multipart/form-data',
          },
      };
      const formData = new FormData();
      for (const key in company) {
        if (Object.prototype.hasOwnProperty.call(company, key)) {
          const element = company[key];
          formData.append(key, element);
        }
      }
      return Axios.post(`./api/Companies`, formData, config); 
 }
 
 public updateCompany(company: any) {
    const config = {
        headers: {
            'content-type': 'multipart/form-data',
          },
      };
      const formData = new FormData();
      for (const key in company) {
        if (Object.prototype.hasOwnProperty.call(company, key)) {
          const element = company[key];
          formData.append(key, element);
        }
      }
      return Axios.put(`./api/Companies/${company.id}`, formData, config); 
   
 }
 
 public getCompanies() {
     return Axios.get('./api/Companies/all');
 }

 public deleteCompany(companyId: number) {
     return Axios.delete(`./api/Companies/${companyId}`);
 }


}

export const companiesService = new Companies();