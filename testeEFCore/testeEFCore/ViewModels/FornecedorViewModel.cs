using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using testeEFCore.Business.Notificacoes;

namespace testeEFCore.ViewModels
{
    public class FornecedorViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Documento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int TipoFornecedor { get; set; }    

        public EnderecoViewModel Endereco { get; set; }

        public bool Ativo { get; set; }

        public string NomeTipoFornecedor { get; set; }

        public List<Notificacao> Mensagens { get; set; }

        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
    }
}
