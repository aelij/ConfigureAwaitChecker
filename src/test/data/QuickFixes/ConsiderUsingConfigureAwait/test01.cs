using System.Threading.Tasks;

class C
{
  public async void Main()
  {
    await{caret} Task.Delay(1);
  }
}