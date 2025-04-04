// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BlazorOverview.Pages
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\_Imports.razor"
using System.Net.Http

#nullable disable
    ;
#nullable restore
#line 2 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\_Imports.razor"
using Microsoft.AspNetCore.Authorization

#nullable disable
    ;
#nullable restore
#line 3 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization

#nullable disable
    ;
#nullable restore
#line 4 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms

#nullable disable
    ;
#nullable restore
#line 5 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing

#nullable disable
    ;
#nullable restore
#line 6 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\_Imports.razor"
using Microsoft.AspNetCore.Components.Web

#nullable disable
    ;
#nullable restore
#line 7 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\_Imports.razor"
using Microsoft.JSInterop

#nullable disable
    ;
#nullable restore
#line 8 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\_Imports.razor"
using BlazorOverview

#nullable disable
    ;
#nullable restore
#line 9 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\_Imports.razor"
using BlazorOverview.Shared

#nullable disable
    ;
#nullable restore
#line 10 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\_Imports.razor"
using Blazored

#nullable disable
    ;
#nullable restore
#line 11 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\_Imports.razor"
using Blazored.Modal

#nullable disable
    ;
#nullable restore
#line 12 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\_Imports.razor"
using Blazored.Modal.Services

#nullable disable
    ;
#nullable restore
#line 1 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\Pages\MyNotes.razor"
 using BlazorOverview.Models

#nullable disable
    ;
#nullable restore
#line 4 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\Pages\MyNotes.razor"
 using BlazorOverview.Services

#nullable disable
    ;
    #line default
    #line hidden
    #nullable restore
    public partial class MyNotes : global::Microsoft.AspNetCore.Components.ComponentBase
    #nullable disable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 81 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\Pages\MyNotes.razor"
       
    // 儲存要顯示的集合清單內的所有紀錄
    public List<MyNote> Notes { get; set; } = new List<MyNote>();

    // 正在編輯或者新增的紀錄物件
    public MyNote CurrentMyNote { get; set; } = new MyNote();
    // 記錄下修改前的紀錄物件
    public MyNote OrigMyNote { get; set; } = new MyNote();
    // 是否要顯示新增或修改對話窗
    public bool ShowPopup { get; set; } = false;
    // 這次處理的紀錄是否為新增，若該屬性為 false，則表示這次要修改紀錄
    public bool IsNewMode { get; set; }
    // 要顯示該新增與修改對話窗的 Id 名稱
    public string DialogIdName { get; set; } = "myModal";

    // 元件建立的時候，所要執行的初始化工作，這裡使用非同步方式來呼叫
    protected override async Task OnInitializedAsync()
    {
        // 透過 IMyNoteService.RetriveAsync() 方法，來取得所有記事清單
        Notes = await MyNoteService.RetriveAsync();
    }
    // 刪除按鈕的點選事件之處理委派方法
    private void Delete(MyNote note)
    {
        CurrentMyNote = note;
        var parameters = new ModalParameters();
        // 宣告要傳遞給刪除對話窗的參數
        parameters.Add("RecordTitleName", note.Title);

        // 綁定使用者關閉對話窗的觸發事件
        Modal.OnClose += ModalClosed;
        // 顯示這個刪除對話窗
        Modal.Show<ConfirmDelete>("確認要刪除嗎？", parameters);
    }
    // 刪除對話窗的事件被觸發所要執行的委派方法
    async void ModalClosed(ModalResult modalResult)
    {
        // 檢查使用者是否有點選確認按鈕
        if (modalResult.Cancelled)
        {
            Console.WriteLine("Modal was Cancelled");
        }
        else
        {
            // 若使用者點選了確定按鈕
            Console.WriteLine(modalResult.Data.ToString());
            await DeleteIt(CurrentMyNote);
            StateHasChanged();
        }
        //解除該對話窗所綁定的事件
        Modal.OnClose -= ModalClosed;
    }
    // 刪除這個筆記事紀錄
    private async Task DeleteIt(MyNote note)
    {
        // 透過 IMyNoteService.DeleteAsync() 方法，從集合清單中刪除所選擇的紀錄
        await MyNoteService.DeleteAsync(note);
        Notes = await MyNoteService.RetriveAsync();
    }

    // 新增按鈕的點選事件之處理委派方法
    private async void Add()
    {
        // 設定此次要進行新增一筆紀錄
        IsNewMode = true;
        //產生要新增的紀錄物件
        CurrentMyNote = new MyNote();
        // 顯示新增紀錄使用的對話窗
        await jsRuntime.InvokeAsync<object>("ShowModal", DialogIdName);
    }
    // 修改按鈕的點選事件之處理委派方法
    private async void Update(MyNote note)
    {
        // 設定此次要進行修改紀錄
        IsNewMode = false;
        CurrentMyNote = OrigMyNote = note.Clone();
        //產生一個物件副本，做為要修改的紀錄物件
        CurrentMyNote = note.Clone();
        //保存此次要修改的紀錄物件
        OrigMyNote = note;
        // 顯示新增紀錄使用的對話窗
        await jsRuntime.InvokeAsync<object>("ShowModal", DialogIdName);
    }
    // 關閉對話窗的方法
    private async void CloseDialog()
    {
        // 設定要關閉對話窗的變數，讓對話窗的 HTML 標記不會顯示在瀏覽器畫面上
        await jsRuntime.InvokeAsync<object>("CloseModal", DialogIdName);
    }
    // 當使用者連完資料資料且點選 儲存 按鈕之後，若沒有錯誤產生，就會觸發此委派方法
    private async void HandleValidSubmit()
    {
        // 設定要關閉對話窗的變數，讓對話窗的 HTML 標記不會顯示在瀏覽器畫面上
        await jsRuntime.InvokeAsync<object>("CloseModal", DialogIdName);
        //判斷此次動作是要新增還是要修改紀錄
        if (IsNewMode == true)
        {
            // 透過 IMyNoteService.CreateAsync() 方法，加入一筆紀錄到集合清單內
            await MyNoteService.CreateAsync(CurrentMyNote);
        }
        else
        {
            // 透過 IMyNoteService.UpdateAsync() 方法，修改該紀錄
            await MyNoteService.UpdateAsync(OrigMyNote, CurrentMyNote);
        }
        // 透過 IMyNoteService.RetriveAsync() 方法，來取得所有記事清單
        Notes = await MyNoteService.RetriveAsync();
        StateHasChanged();
    }

#line default
#line hidden
#nullable disable

        [global::Microsoft.AspNetCore.Components.InjectAttribute] private 
#nullable restore
#line 10 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\Pages\MyNotes.razor"
        IModalService

#line default
#line hidden
#nullable disable
         
#nullable restore
#line 10 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\Pages\MyNotes.razor"
                      Modal

#line default
#line hidden
#nullable disable
         { get; set; }
         = default!;
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private 
#nullable restore
#line 8 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\Pages\MyNotes.razor"
        IJSRuntime

#line default
#line hidden
#nullable disable
         
#nullable restore
#line 8 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\Pages\MyNotes.razor"
                   jsRuntime

#line default
#line hidden
#nullable disable
         { get; set; }
         = default!;
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private 
#nullable restore
#line 6 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\Pages\MyNotes.razor"
        IMyNoteService

#line default
#line hidden
#nullable disable
         
#nullable restore
#line 6 "C:\Users\nealw\Documents\BlazorApp\BlazorOverview\Pages\MyNotes.razor"
                       MyNoteService

#line default
#line hidden
#nullable disable
         { get; set; }
         = default!;
    }
}
#pragma warning restore 1591
