# FleetManager Solution

## Descrição

Aplicação web desenvolvida em .NET para gerenciamento de uma frota de veículos, incluindo ônibus, caminhões e carros.

Cada veículo possui os seguintes atributos:

- **Chassis Id**  
  - Chassis Series (string)  
  - Chassis Number (uint)  
- **Tipo**: Pode ser Bus, Truck ou Car  
- **Número de passageiros**:  
  - Caminhões: sempre 1  
  - Ônibus: sempre 42  
  - Carros: sempre 4  
- **Cor** (string)

### Funcionalidades

- Inserir novo veículo (validação para chassis duplicado)  
- Editar veículo existente (alterar apenas a cor, buscando pelo chassis id)  
- Listar todos os veículos  
- Buscar veículo por chassis id  

---

## Pré-requisitos

- Docker instalado no computador local  
- **Opcional:** Visual Studio (com suporte a Docker Compose)  

---

## Como rodar

### Com Visual Studio

1. Abrir a solução no Visual Studio.  
2. Selecionar a opção para rodar usando **Docker Compose**.  
3. Executar a aplicação.

### Sem Visual Studio

No terminal, dentro da pasta onde está o arquivo `docker-compose.yml`, execute:

```bash
# Construir as imagens Docker (caso necessário)
docker-compose build

# Subir os containers da aplicação
docker-compose up
