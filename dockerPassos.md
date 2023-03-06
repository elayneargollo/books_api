1. Após intatalações e configurações verifique se o ambiente está pronto:

   - Fora da pasta BookStoreApi, onde se encontra o dockerfile, digite o seguinte comando:

         docker build -t <nome_imagem> .
         
2. Verificar se a imagem foi criada:

   - Insira o comando no terminal.

         docker build -t <nome_imagem> .
         
 - Resultado esperado

   - REPOSITORY <nome_imagem>: tag: image id
   
3. Suba o docker compose ao digitar:

   - Insira o comando no terminal.

         docker compose up -d

4. Os seguintes links estarão disponíveis:

        - Grafana: localhost:3000
        - Prometheus: localhost:9090
        - Aplicação: localhost:8090/swagger/index.html
