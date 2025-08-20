# Sistema de Pedidos em Console (POO)

### Tecnologias Utilizadas

- **Linguagem:** C#
- **Framework:** .NET 8.0

### Sobre o Projeto

Este sistema de pedidos em C# foi desenvolvido como projeto para a disciplina de Programação Orientada a Objetos pela Universidade de Caxias do Sul sob a orientação do Prof. Leonardo Pellizzoni. O objetivo foi aplicar na prática os conceitos de POO, arquitetura em camadas e boas práticas de desenvolvimento para criar uma aplicação de console completa e funcional.

### Principais Funcionalidades

- **Login Diferenciado:** Menus e permissões diferentes para usuários **Administradores** e **Clientes**.
- **Módulo Admin:**
    - CRUD de Produtos, Fornecedores e Transportadoras.
    - Consulta de todos os pedidos, com a capacidade de alterar seu status.
- **Módulo Cliente:**
    - Busca de produtos por nome ou código.
    - Criação de carrinho de compras.
    - Realização de pedidos com validação de estoque em tempo real.
    - Consulta de histórico de pedidos pessoais, com todos os detalhes de cada compra.

### Arquitetura e Conceitos Aplicados

Para construir o sistema, além dos requisitos básicos, foram aplicados alguns conceitos com foco em uma arquitetura limpa e organizada:

- **Arquitetura em Camadas:** Separada a aplicação em UI (Menus), Serviços (lógica de negócio) e Repositórios (acesso a dados), para manter o código desacoplado e fácil de manter.
- **Repository Pattern:** Este padrão foi utilizado para abstrair toda a manipulação dos dados (que são salvos em arquivos .json), permitindo que a camada de negócio não precise saber como os dados são salvos.
- **Uso de Interfaces:** Definidos contratos com interfaces para garantir o baixo acoplamento entre as camadas, facilitando a modularidade do sistema.
- **Persistência de Dados:** Foi implementado a leitura e escrita de todos os dados em arquivos .json, simulando o comportamento de um banco de dados.
- **Tratamento de Exceções:** Implementado o lançamento de exceções customizadas para tratar erros de regras de negócio, como a tentativa de venda de um produto sem estoque.