const express = require('express');
const mysql = require('mysql2');
const cors = require('cors');
const app = express();
const PORT = 3000;

// Configuração da conexão com o banco de dados

const db = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: 'Vid@l9871',
    database: 'acompanha_pedidosschema'
});

// Conectar ao banco de dados
db.connect(err => {
    if (err) throw err;
    console.log('Conectado ao banco de dados!');
});

// Habilitar CORS
app.use(cors());

// Habilitar o middleware para parsing do corpo das requisições
app.use(express.json()); // Mover essa linha para cima

// Endpoint para obter os produtos disponiveis
app.get('/api/produtos/:usuario_nome', (req, res) => {
    const usuario = req.params.usuario_nome;
    console.log(`Requisição de produtos do usuario: ${usuario}`)
    db.query(`SELECT * FROM produtos WHERE usuario = '${usuario}'`, (err, results) => {
        if (err) throw err;
        res.json(results);
    });
});

// Iniciar o servidor
app.listen(PORT, () => {
    console.log(`Servidor rodando na porta ${PORT}`);
});