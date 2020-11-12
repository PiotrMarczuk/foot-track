import { TrainingMeasurement } from '@/models/TrainingMeasurement';
import axios from "axios";
import { google } from 'google-maps';
const API_URL = process.env.VUE_APP_APIURL + "trainings";

export class TrainingService {
    async startTraining(userId: string) {
        const response = await axios.post(API_URL + "/start",
            {
                id: userId
            });

        return response.data;
    }

    async endTraining(userId: string) {
        const response = await axios.post(API_URL + "/end", {
            id: userId
        }).then(data => console.log(data))
            .catch(error => console.log(error))

        return response;
    }

    async getTrainings(userId: string, pageNumber: number, pageSize: number) {
        const response = await axios.post(API_URL, {
            userId: userId,
            pageNumber: pageNumber,
            pageSize: pageSize,
        })

        return response.data;
    }

    async getTraining(trainingId: string) {
        const response = await axios.get(API_URL + `/${trainingId}`);

        return response.data;
    }


    async sendTrainingData(trainingId: string, data: Array<TrainingMeasurement>) {
        const response = await axios.post(API_URL + "/trainingData", {
            TrainingId: trainingId,
            TrainingRecords: data.map(x => {
                return {
                    Latitude: x.point.lat,
                    Longitude: x.point.lng,
                    Speed: x.speed,
                    Timestamp: x.timestamp,
                };
            })
        });

        return response.data;
    }
}
