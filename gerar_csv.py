import pandas as pd
import uuid
import csv

# Configurações
ESTADOS_URL = "https://raw.githubusercontent.com/kelvins/Municipios-Brasileiros/main/csv/estados.csv"
CIDADES_URL = "https://raw.githubusercontent.com/kelvins/Municipios-Brasileiros/main/csv/municipios.csv"

def generate_guid(code):
    return str(uuid.uuid5(uuid.NAMESPACE_OID, str(code)))

# Processar Estados
estados = pd.read_csv(ESTADOS_URL)
estados["Id"] = estados["codigo_uf"].apply(generate_guid)
estados = estados.rename(columns={
    "nome": "Nome",
    "uf": "Sigla"
})

# Salvar CSV de Estados (sem aspas nos IDs)
estados[["Id", "Nome", "Sigla"]].to_csv(
    "estados.csv",
    index=False,
    encoding='utf-8-sig',
    sep=';',
    quoting=csv.QUOTE_MINIMAL  # Só coloca aspas quando necessário
)

# Processar Cidades
cidades = pd.read_csv(CIDADES_URL)
cidades["Id"] = cidades["codigo_ibge"].apply(generate_guid)
estado_id_map = dict(zip(estados["codigo_uf"], estados["Id"]))
cidades["EstadoId"] = cidades["codigo_uf"].map(estado_id_map)
cidades = cidades.rename(columns={"nome": "Nome"})

# Salvar CSV de Cidades (sem aspas nos IDs)
cidades[["Id", "Nome", "EstadoId"]].to_csv(
    "cidades.csv",
    index=False,
    encoding='utf-8-sig',
    sep=';',
    quoting=csv.QUOTE_MINIMAL
)

print("Arquivos gerados: estados.csv e cidades.csv (IDs sem aspas)")