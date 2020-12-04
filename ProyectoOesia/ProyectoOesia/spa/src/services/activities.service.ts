import Axios from 'axios';

class Activities {
 
 public getActivities() {
     return Axios.get('./api/Activities');
 }

}

export const activitiesService = new Activities();