namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.States;

internal abstract class ImageWorkerState
{
    protected MainWindow Context;

    public ImageWorkerState(MainWindow context)
    {
        Context = context;
    }

    public abstract void Open();

    public abstract void Convert();

    public abstract void Substract();
}
