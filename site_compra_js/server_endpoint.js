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

// Endpoint para inserir um novo produto
app.post('/api/produtos', (req, res) => {
    const { nome_cliente, endereco, produtos_nome,observacoes,hora_pedido,valorTotal,formaPag,valorLiq,usuario,delivery,pagamento_aprovado } = req.body;
    // Validando se todos os campos estão presentes
    const sql = `INSERT INTO pedidos(nome_cliente,endereco,produtos_nome,observacoes,hora_pedido,valorTotal,formaPag,valorLiq,usuario,delivery,pagamento_aprovado) VALUES('{nome_cliente}','{endereco}', '{produtos}', '{obs}', '{data}', {valor},'{formaPag}',{valor},'${usuarios}',{delivery},{pagamento_efetuado});`
    db.query(sql, (err, result) => {
        if (err) {
            console.error('Erro ao inserir produto no banco de dados:', err);
            return res.status(500).json({ error: 'Erro no servidor' });
        }
        // Responde com uma mensagem de sucesso e o ID do novo produto
        res.status(201).json({ message: 'Produto inserido com sucesso', produtoId: result.insertId });
    });
});

// Iniciar o servidor
app.listen(PORT, () => {
    console.log(`Servidor rodando na porta ${PORT}`);
});