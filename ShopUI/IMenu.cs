namespace ShopUI
{
    /* 
        Interface that implements abstraction.
        Every method is implicitly abstract and public.
    */
    public interface IMenu
    {
        /// <summary>
        /// Will dsiplay the menu and user choices in the terminal.
        /// </summary>
        void Display(); //implicitly public


        /// <summary>
        /// Will record the user choice and change/route your menu based on your choice.
        /// </summary>
        /// <returns>Return the menu that will change your screen.</returns>
        string UserChoice();
    }

}