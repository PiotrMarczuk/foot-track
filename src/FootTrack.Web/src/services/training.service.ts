import axios from "axios";
const API_URL = process.env.VUE_APP_APIURL + "trainings/";

export class TrainingService {
    async startTraining(userId: string) {
        const response = await axios.post(API_URL + "start",
            {
                id: userId
            }).then(data => console.log(data))
            .catch(error => console.log(error))
    }

    async endTraining(userId: string) {
        const response = await axios.post(API_URL + "end", {
            id: userId
        }).then(data => console.log(data))
            .catch(error => console.log(error))
    }
}
