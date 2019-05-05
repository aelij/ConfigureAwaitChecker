using System.Threading.Tasks;

class C
{
  public async void Main()
  {
    await new ValueTask(){caret};
  }
}