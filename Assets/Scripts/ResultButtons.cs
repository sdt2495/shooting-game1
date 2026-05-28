using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButtons : MonoBehaviour
{
    // リトライ（ゲーム再スタート）
    public void OnRetryButton()
    {
        SceneManager.LoadScene("BattleScene");   // ← あなたのゲームシーン名に合わせて変更
    }

    // タイトルへ戻る
    public void OnTitleButton()
    {
        SceneManager.LoadScene("TitleScene");  // ← タイトルシーン名に合わせて変更
    }
}
