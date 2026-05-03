using FluentValidation;
using FluentValidation.Results;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Validations;

public class ProdutoValidation: AbstractValidator<ProdutoModel>
{
    public ProdutoValidation()
    {
        RuleFor(p => p.Descricao)
            .NotEmpty().WithMessage("A descrição do produto é obrigatória.")
            .MaximumLength(100).WithMessage("A descrição do produto deve ter no máximo 100 caracteres.");
        RuleFor(p => p.Quantidade)
            .GreaterThan(0).WithMessage("A quantidade do produto deve ser maior que zero.");
        RuleFor(p => p.Preco)
            .GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.");
    }

    public override async Task<ValidationResult> ValidateAsync(ValidationContext<ProdutoModel> context, CancellationToken cancellation = default)
    {
        if (context == null || context.InstanceToValidate == null)
        {
            return new ValidationResult(new[] { new ValidationFailure("ProdutoModel", "O objeto ProdutoModel é nulo.") });
        }
        return await base.ValidateAsync(context, cancellation);
    }

    public override ValidationResult Validate(ValidationContext<ProdutoModel> context)
    {
        if (context == null || context.InstanceToValidate == null)
        {
            return new ValidationResult(new[] { new ValidationFailure("ProdutoModel", "O objeto ProdutoModel é nulo.") });
        }

        return base.Validate(context);
    }
}
