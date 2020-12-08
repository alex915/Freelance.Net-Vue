import Axios from 'axios';

class CompanyService {
    
    //llama a la api mediante axios y devuelve la promesa
    public search(activity: string) {
        return Axios.get(`./api/companies?activity=${activity}`);
    }

    public filter(company: string, activity: string) {
        return Axios.post(`./api/companies?company=${company}&&.activity=${activity}`, {
            company,
            activity,
        });
    }
}

export const companyService = new CompanyService();