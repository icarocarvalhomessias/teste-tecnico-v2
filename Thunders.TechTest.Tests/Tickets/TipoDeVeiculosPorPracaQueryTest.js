import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 100, // Número de usuários virtuais
    duration: '30s', // Duração do teste
};

const pedagioIds = [
    '30b2d1e8-bc63-4607-b779-0492f27ed804',
    'b6c1debd-ed00-4c40-a814-13bcf6ed388f',
    '4b6169ca-e7f3-419e-8cbf-16c831a955db',
    '58180a40-7a55-4615-8dfc-1fde2d0edd06',
    'aaf1177e-592d-4d5f-aa63-2569ac7a6497',
    'c416cbcb-d4cd-432f-bcd3-27a32538a7e9',
    '229ef864-17f1-4aa7-ada5-2f6a38f8e513',
    '9cd04400-353a-4fc3-af6b-32d5cd009bcb',
    '78f0a8fa-fbe4-498d-a292-40ed3f7382f3',
    '737f8daf-ab8c-41fa-b2b6-467c1e11f235',
    '6864676e-df2d-4045-bf6a-4806fa9a826f',
    '53c1c8da-104c-4c76-b4eb-5109330f1af7',
    '926463e8-bed7-4ebb-9737-510a1967196b',
    '7f490bae-4eeb-4994-b007-610140919660',
    '3c817bae-dd44-4f95-80bf-610d466e24c5',
    'a782874f-9fc2-47aa-abe9-6a3aaf4bdd86',
    '9614b870-405e-4ce1-bd4c-6eca985b02d5',
    '9a289040-f5e7-42c6-9879-80e1ba228847',
    '1c819afa-d711-4dde-b229-8e18be4fba11',
    'c68b950f-503a-4a34-8f64-8ff8523520cf',
    '556ac698-9663-4e75-a8a3-998ea5fcbe1d',
    'aaa2c3bd-0136-45eb-a43f-a18e2991ada4',
    '4d42d7f1-ab44-4c10-8e3d-bd43bd7aa1c7',
    'd3b0fc15-fb00-478c-adb9-e05675505347',
    '9180ec28-1767-4267-ac5e-f1f01c3e592a',
    'd9b398e5-04da-4a3d-8b01-fb84c6b914c0',
    '3a211f2b-68ba-4da9-ae94-fe9b70fe532c'
];

export default function () {
    let randomPedagioId = pedagioIds[Math.floor(Math.random() * pedagioIds.length)];
    let url = `https://localhost:7405/api/Ticket/tipo-de-veiculos-por-praca?pracaId=${randomPedagioId}`;

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

