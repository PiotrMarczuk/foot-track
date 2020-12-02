import { google } from 'google-maps';

export class TrainingMeasurement {
    speed: number;
    timestamp: Date;
    point: google.maps.LatLng;

    constructor(point: google.maps.LatLng, speed: number, timestamp: Date) {
        this.speed = speed;
        this.timestamp = timestamp;
        this.point = point;
    }
  }
  