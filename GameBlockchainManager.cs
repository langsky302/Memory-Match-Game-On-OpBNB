using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.UI;
using TMPro;

public class BlockchainManager : MonoBehaviour
{
    public string Address { get; private set; }
    public Button StartBtn;
    public Text StartBtnText;
    public Button NFTBtn;
    public Text NFTBtnText;
    public Button x2TimeBtn;
    public Text x2TimeBtnText;

    private void Start()
    {
        StartBtn.gameObject.SetActive(false);
        NFTBtn.gameObject.SetActive(false);
        x2TimeBtn.gameObject.SetActive(false);
    }

    public async void CheckOpBNBGameKey()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        //For Testing
        Address = "0x4C6F5f4e21840f1e103fF8791AC70b4Ff1AD59f9";
        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x0e789B74C06D706900BEb4677398Fa233F9cb17b",
            "[{\"type\":\"event\",\"name\":\"doubleTimeClaimed\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newClaim\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"opBNBKeyGiven\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"opBNBTokenClaimed\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newCount\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"function\",\"name\":\"claimOpBNBKey\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"getDoubleTime\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getOpBNBToken\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"giveDoubleTime\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"giveOpBNBToken\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"hasOpBNBKey\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"bool\",\"name\":\"\",\"internalType\":\"bool\"}],\"stateMutability\":\"view\"}]"
            );
        bool ishaveOpBNBGameKey = await contract.Read<bool>("hasOpBNBKey", Address);

        if (ishaveOpBNBGameKey == true)
        {
            StartBtn.gameObject.SetActive(true);
            x2TimeBtn.gameObject.SetActive(true);
            NFTBtn.gameObject.SetActive(false);

        }
        else
        {
            StartBtn.gameObject.SetActive(false);
            x2TimeBtn.gameObject.SetActive(false);
            NFTBtn.gameObject.SetActive(true);
        }
    }
    public async void GetGameKey()
    {
        Debug.Log("GetGameKey");
        NFTBtn.interactable = false;
        NFTBtnText.text = "Getting Key";

        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();

        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x0e789B74C06D706900BEb4677398Fa233F9cb17b",
            "[{\"type\":\"event\",\"name\":\"doubleTimeClaimed\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newClaim\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"opBNBKeyGiven\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"opBNBTokenClaimed\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newCount\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"function\",\"name\":\"claimOpBNBKey\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"getDoubleTime\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getOpBNBToken\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"giveDoubleTime\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"giveOpBNBToken\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"hasOpBNBKey\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"bool\",\"name\":\"\",\"internalType\":\"bool\"}],\"stateMutability\":\"view\"}]"
            );

        await contract.Write("claimOpBNBKey", Address);

        StartBtn.gameObject.SetActive(true);
        x2TimeBtn.gameObject.SetActive(true);
        NFTBtn.gameObject.SetActive(false);

        NFTBtn.interactable = true;
        NFTBtnText.text = "Token Gate";
    }

    public async void GetopBNBToken()
    {
        Debug.Log("GetopBNBToken");
        StartBtn.interactable = false;
        x2TimeBtn.interactable = false;
        StartBtnText.text = "Preparing!";

        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();

        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x0e789B74C06D706900BEb4677398Fa233F9cb17b",
            "[{\"type\":\"event\",\"name\":\"doubleTimeClaimed\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newClaim\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"opBNBKeyGiven\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"opBNBTokenClaimed\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newCount\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"function\",\"name\":\"claimOpBNBKey\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"getDoubleTime\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getOpBNBToken\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"giveDoubleTime\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"giveOpBNBToken\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"hasOpBNBKey\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"bool\",\"name\":\"\",\"internalType\":\"bool\"}],\"stateMutability\":\"view\"}]"
            );

        await contract.Write("giveOpBNBToken", Address);

        StartBtn.interactable = true;
        x2TimeBtn.interactable = true;
        StartBtnText.text = "Start";

        //Start Game
        GameManager.Ins.PlayGame();
    }

    public async void Getx2Time()
    {
        Debug.Log("Getx2Time");
        StartBtn.interactable = false;
        x2TimeBtn.interactable = false;
        x2TimeBtnText.text = "Getting x2Time";

        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();

        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x0e789B74C06D706900BEb4677398Fa233F9cb17b",
            "[{\"type\":\"event\",\"name\":\"doubleTimeClaimed\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newClaim\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"opBNBKeyGiven\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"event\",\"name\":\"opBNBTokenClaimed\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"indexed\":true,\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"newCount\",\"indexed\":false,\"internalType\":\"uint256\"}],\"outputs\":[],\"anonymous\":false},{\"type\":\"function\",\"name\":\"claimOpBNBKey\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"getDoubleTime\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"getOpBNBToken\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"giveDoubleTime\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"giveOpBNBToken\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"},{\"type\":\"function\",\"name\":\"hasOpBNBKey\",\"inputs\":[{\"type\":\"address\",\"name\":\"player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"bool\",\"name\":\"\",\"internalType\":\"bool\"}],\"stateMutability\":\"view\"}]"
            );

        await contract.Write("giveDoubleTime", Address);

        StartBtn.interactable = true;
        x2TimeBtn.interactable = true;
        x2TimeBtnText.text = "x2 Times";

        //x2 Time code
        if (GameManager.Ins.timeLimit == 120) {
            GameManager.Ins.timeLimit *= 2;
            GameManager.Ins.m_timeCounting = GameManager.Ins.timeLimit;
        }
       
    }
}
