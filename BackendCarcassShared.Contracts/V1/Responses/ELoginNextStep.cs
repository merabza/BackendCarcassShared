using System.Text.Json.Serialization;

namespace BackendCarcassShared.Contracts.V1.Responses;

//რა უნდა გააკეთოს მომხმარებელმა ავტორიზაციის დასასრულებლად
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ELoginNextStep
{
    //ავტორიზაცია დასრულებულია, Token სრული წვდომის ტოკენია
    None = 0,

    //ელფოსტაზე გაგზავნილია კოდი და ბმული, Token დროებითი ტოკენია მხოლოდ დადასტურების ნაბიჯისთვის
    ConfirmEmail = 1,

    //პაროლი მოქმედ წესებს არ აკმაყოფილებს, უნდა შეიცვალოს. Token დროებითი ტოკენია მხოლოდ ამ ნაბიჯისთვის
    ChangePassword = 2,

    //ელფოსტა ბმულით დადასტურდა სხვა ბრაუზერში, ავტორიზაცია თავიდან უნდა გაიაროს. Token ცარიელია
    EmailConfirmedLoginRequired = 3
}
