import { UserService } from "./user.service";
import {TrainingService} from "./training.service";

export const userService: UserService = new UserService();
export const trainingService: TrainingService = new TrainingService();
