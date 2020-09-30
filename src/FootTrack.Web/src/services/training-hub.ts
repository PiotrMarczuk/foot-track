import { HubConnectionBuilder, LogLevel } from "@aspnet/signalr";

export default {
    install(Vue : any) {
        const connection = new HubConnectionBuilder()
            .withUrl(process.env.VUE_APP_HUBURL || "")
            .configureLogging(LogLevel.Information)
            .build()

        let startedPromise = null;
        function start() {
            startedPromise = connection.start().catch(err => {
                console.error('Failed to connect with hub', err)
                return new Promise((resolve, reject) =>
                    setTimeout(() => start().then(resolve).catch(reject), 5000));
            })
            return startedPromise;
        }
        connection.onclose(() => start());

        start();

        const trainingHub = new Vue();
        Vue.prototype.$trainingHub = trainingHub;

        connection.on('QuestionScoreChange', (questionId, score) => {
            trainingHub.$emit('score-changed', { questionId, score })
        });
    }

}
