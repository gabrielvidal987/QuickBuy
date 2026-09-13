-- ============================================================================
-- Migração: taxas de débito/crédito fixas por usuário (tela de Relatórios)
-- ============================================================================

USE acompanha_pedidosschema;

-- Taxa (%) de débito/crédito digitada no relatório fica gravada aqui e é
-- recarregada da próxima vez que a tela abrir, até o usuário digitar outra.
-- NULL = nunca configurada -> usa o padrão do sistema (1,5%).
ALTER TABLE usuarios
    ADD COLUMN taxa_debito DECIMAL(5,2) NULL
        COMMENT 'Taxa % de débito fixada no relatório; NULL = usa o padrão de 1,5%',
    ADD COLUMN taxa_credito DECIMAL(5,2) NULL
        COMMENT 'Taxa % de crédito fixada no relatório; NULL = usa o padrão de 1,5%';
