using Core.V1.DTOs;
using Core.V1.Validators.GameRequest;

public class GameRequestInputValidator() : SingleValidator<GameRequestInputModel>() { }
public class GameRequestListInputValidator() : ListValidator<GameRequestInputModel>() { }