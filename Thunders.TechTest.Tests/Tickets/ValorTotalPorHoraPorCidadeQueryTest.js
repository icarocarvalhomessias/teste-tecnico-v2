import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 100, // Número de usuários virtuais
    duration: '30s', // Duração do teste
};


export default function () {
    let url = `https://localhost:7405/api/Ticket/valor-total-por-hora-por-cidade`;

    let params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    let res = http.get(url, params);

    check(res, {
        'is status 200': (r) => r.status === 200,
    });

    sleep(1);
}
