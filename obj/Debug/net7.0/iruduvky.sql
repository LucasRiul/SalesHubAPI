IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [EMPRESAS] (
    [idEmpresa] int NOT NULL IDENTITY,
    [CNPJ] nvarchar(17) NOT NULL,
    [CEP] nvarchar(10) NOT NULL,
    [Logradouro] nvarchar(20) NOT NULL,
    [Endereco] nvarchar(100) NOT NULL,
    [Bairro] nvarchar(100) NOT NULL,
    [UF] nvarchar(2) NOT NULL,
    [Pais] nvarchar(50) NOT NULL,
    [Nome] nvarchar(100) NOT NULL,
    [NomeFantasia] nvarchar(100) NOT NULL,
    [RegimeTributario] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_EMPRESAS] PRIMARY KEY ([idEmpresa])
);
GO

CREATE TABLE [FORNECEDORES] (
    [idFornecedor] bigint NOT NULL IDENTITY,
    [Nome] nvarchar(150) NOT NULL,
    [CPNJ] nvarchar(17) NOT NULL,
    [Telefone1] nvarchar(30) NOT NULL,
    [Telefone2] nvarchar(30) NOT NULL,
    [CEP] nvarchar(10) NOT NULL,
    [Logradouro] nvarchar(20) NOT NULL,
    [Endereco] nvarchar(100) NOT NULL,
    [Bairro] nvarchar(100) NOT NULL,
    [UF] nvarchar(2) NOT NULL,
    [Pais] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_FORNECEDORES] PRIMARY KEY ([idFornecedor])
);
GO

CREATE TABLE [VENDAS] (
    [idVenda] bigint NOT NULL IDENTITY,
    [DataVenda] datetime2 NOT NULL,
    [ValorComDesconto] real NOT NULL,
    [Desconto] real NOT NULL,
    CONSTRAINT [PK_VENDAS] PRIMARY KEY ([idVenda])
);
GO

CREATE TABLE [USUARIOS] (
    [idUsuario] int NOT NULL IDENTITY,
    [CPF] nvarchar(14) NOT NULL,
    [DataNascimento] datetime2 NULL,
    [Nome] nvarchar(150) NOT NULL,
    [Nivel] nvarchar(30) NOT NULL,
    [Login] nvarchar(30) NOT NULL,
    [Senha] nvarchar(30) NOT NULL,
    [Status] bit NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [idEmpresa] int NOT NULL,
    [EmpresaidEmpresa] int NOT NULL,
    CONSTRAINT [PK_USUARIOS] PRIMARY KEY ([idUsuario]),
    CONSTRAINT [FK_USUARIOS_EMPRESAS_EmpresaidEmpresa] FOREIGN KEY ([EmpresaidEmpresa]) REFERENCES [EMPRESAS] ([idEmpresa]) ON DELETE CASCADE
);
GO

CREATE TABLE [PRODUTOS] (
    [idProduto] bigint NOT NULL IDENTITY,
    [Nome] nvarchar(50) NOT NULL,
    [EstoqueMinimo] int NOT NULL,
    [EstoqueInicial] int NOT NULL,
    [EstoqueAtual] int NOT NULL,
    [PrecoCusto] real NOT NULL,
    [PrecoVenda] real NOT NULL,
    [LUCRO] real NOT NULL,
    [ICMS] real NOT NULL,
    [ISS] real NOT NULL,
    [COFINS] real NOT NULL,
    [Comissao] real NOT NULL,
    [idUsuario] bigint NOT NULL,
    [idFornecedor] bigint NOT NULL,
    [UsuarioidUsuario] int NOT NULL,
    [FornecedoridFornecedor] bigint NOT NULL,
    CONSTRAINT [PK_PRODUTOS] PRIMARY KEY ([idProduto]),
    CONSTRAINT [FK_PRODUTOS_FORNECEDORES_FornecedoridFornecedor] FOREIGN KEY ([FornecedoridFornecedor]) REFERENCES [FORNECEDORES] ([idFornecedor]) ON DELETE CASCADE,
    CONSTRAINT [FK_PRODUTOS_USUARIOS_UsuarioidUsuario] FOREIGN KEY ([UsuarioidUsuario]) REFERENCES [USUARIOS] ([idUsuario]) ON DELETE CASCADE
);
GO

CREATE TABLE [LOTES] (
    [idLote] bigint NOT NULL IDENTITY,
    [DataValidade] datetime2 NOT NULL,
    [Quantidade] int NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [idProduto] bigint NOT NULL,
    [ProdutoidProduto] bigint NOT NULL,
    CONSTRAINT [PK_LOTES] PRIMARY KEY ([idLote]),
    CONSTRAINT [FK_LOTES_PRODUTOS_ProdutoidProduto] FOREIGN KEY ([ProdutoidProduto]) REFERENCES [PRODUTOS] ([idProduto]) ON DELETE CASCADE
);
GO

CREATE TABLE [PRODUTO_VENDAS] (
    [idProduto] bigint NOT NULL IDENTITY,
    [idVenda] bigint NOT NULL,
    [Quantidade] int NOT NULL,
    [ProdutoidProduto] bigint NOT NULL,
    [VendaidVenda] bigint NOT NULL,
    CONSTRAINT [PK_PRODUTO_VENDAS] PRIMARY KEY ([idProduto]),
    CONSTRAINT [FK_PRODUTO_VENDAS_PRODUTOS_ProdutoidProduto] FOREIGN KEY ([ProdutoidProduto]) REFERENCES [PRODUTOS] ([idProduto]) ON DELETE CASCADE,
    CONSTRAINT [FK_PRODUTO_VENDAS_VENDAS_VendaidVenda] FOREIGN KEY ([VendaidVenda]) REFERENCES [VENDAS] ([idVenda]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_LOTES_ProdutoidProduto] ON [LOTES] ([ProdutoidProduto]);
GO

CREATE INDEX [IX_PRODUTO_VENDAS_ProdutoidProduto] ON [PRODUTO_VENDAS] ([ProdutoidProduto]);
GO

CREATE INDEX [IX_PRODUTO_VENDAS_VendaidVenda] ON [PRODUTO_VENDAS] ([VendaidVenda]);
GO

CREATE INDEX [IX_PRODUTOS_FornecedoridFornecedor] ON [PRODUTOS] ([FornecedoridFornecedor]);
GO

CREATE INDEX [IX_PRODUTOS_UsuarioidUsuario] ON [PRODUTOS] ([UsuarioidUsuario]);
GO

CREATE INDEX [IX_USUARIOS_EmpresaidEmpresa] ON [USUARIOS] ([EmpresaidEmpresa]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231030155034_initial', N'7.0.13');
GO

COMMIT;
GO

