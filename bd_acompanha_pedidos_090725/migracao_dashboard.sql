-- ============================================================================
-- Migração: dashboards na tela inicial + alerta de estoque mínimo
-- Rodar no MySQL Workbench (faça backup antes se quiser segurança extra).
-- É idempotente o suficiente: se a coluna já existir, o ALTER dá erro e pode
-- ser ignorado.
-- ============================================================================

USE acompanha_pedidosschema;

-- 1) Estoque mínimo por produto. Quando qtd_estoque <= estoque_minimo (e
--    estoque_minimo > 0), a tela inicial mostra o alerta de estoque baixo.
--    0 = sem alerta para esse produto.
ALTER TABLE produtos
    ADD COLUMN estoque_minimo INT NOT NULL DEFAULT 0
    COMMENT 'Abaixo (ou igual) disso e > 0, dispara o alerta de estoque na tela inicial';

-- 2) Desconto concedido no pedido (em R$). O valor_total já é gravado LÍQUIDO
--    (com o desconto abatido e rateado nos itens); esta coluna guarda o quanto
--    foi o desconto para o dashboard poder mostrar faturamento BRUTO x LÍQUIDO.
--    Pedidos antigos ficam com 0 (não tinham desconto).
ALTER TABLE pedidos
    ADD COLUMN desconto DECIMAL(10,2) NOT NULL DEFAULT 0
    COMMENT 'Desconto concedido no pedido, em R$ (valor_total já está liquido)';
