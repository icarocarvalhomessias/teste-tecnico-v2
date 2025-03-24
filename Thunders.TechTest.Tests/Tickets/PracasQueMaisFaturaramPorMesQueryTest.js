import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 100, // Número de usuários virtuais
    duration: '30s', // Duração do teste
};

function getRandomMonth() {
    return Math.floor(Math.random() * 12) + 1;
}

function getRandomQuantity() {
    return Math.floor(Math.random() * 46) + 5;
}

export default function () {
    let url = `https://localhost:7405/api/Ticket/pracas-que-mais-faturaram-por-mes?ano=2025&mes=${getRandomMonth()}&quantidade=${getRandomQuantity()}`;

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
