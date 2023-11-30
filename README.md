# DockerBiblioteca
Esse projeto foi realizado com a seguinte estrutura:
- Api de uma biblioteca
- RabbitMQ para controle de mensagens
- Projeto RabbitMQ.Consumer: Aplicação console que imprimi a mensagem enviada para a messageria no console.
- Teste unitário utilizando Xunit

## Funcionalidades da API
- Criação de usuário através do controller UserController isso criará também um cliente vinculado a esse usuário. Ao utilizar o usuário criado é possível gerar um token através do endpoint login.
- Através de migration será gerado um banco de dados inicial para a tabela LivroGenero.
- Ao realizar um emprestimo ou uma devolução uma mensagem será enviada para o RabbitMQ
  
## Inicialização da aplicação:
- Realize o clone do projeto através do comando git clone
- Abra o Docker Desktop
- Abra a pasta onde encontra-se o arquivo docker-compose no terminal
- Execute o comando: docker-compose up
- Caso o comando dê o erro desligue o BuildKit através do comando: $env:DOCKER_BUILDKIT=0
- Execute novamente o comando: docker-compose up
- Aguarde até que todos os container subam e o migration rode no banco de dados, isso pode levar alguns minutos
- execute o comando em outro terminal para verificar se todos subiram: docker ps
- Pronto! A aplicação está online

## Portas de acesso
- RabbitMQ - http://localhost:15672/#/
  - usuário: admin senha: admin
- Banco MySQL - http://localhost:33306
  - usuário: root senha: root
- Api - http://localhost:32768/swagger/index.html
