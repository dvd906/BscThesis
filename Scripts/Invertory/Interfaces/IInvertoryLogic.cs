using System;

public interface IInvertoryLogic : IInputLogic<IInvertory>
{
    bool IsChangeNeeded { get; }
}
