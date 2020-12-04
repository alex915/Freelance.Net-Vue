import Axios from 'axios';

class Location {
 
 public getProvince() {
     return Axios.get('https://datos.gob.es/apidata/nti/territory/Province?_sort=label&_pageSize=100');
 }

}

export const locationService = new Location();