import Axios from 'axios';

class SearchService {
    
    //llama a la api mediante axios y devuelve la promesa
    public searching(q: string) {
        return Axios.get(`./api/search?${q}`);
    }

 
}

export const searchService = new SearchService();