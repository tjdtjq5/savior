﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loding : MonoBehaviour
{
    public string nextScene;
    public Slider progressbar;
    public Text loadtext;
    public GameObject progress;
    public GameObject start;

    public void Start()
    {
        LoadScene();
        start.SetActive(false);
    }



    public void LoadScene()
    {
        progressbar.gameObject.SetActive(true);
        progressbar.value = 0;
        loadtext.gameObject.SetActive(true);
        StartCoroutine(LoadSceneCoroutine());
    }

    IEnumerator LoadSceneCoroutine()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;

            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
                loadtext.text = ((int)(progressbar.value * 100)).ToString() + "%";
            }
            else if (operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
                loadtext.text = ((int)(progressbar.value * 100)).ToString() + "%";
            }



            if (progressbar.value >= 1f)
            {
                progress.SetActive(false);
                start.SetActive(true);
            }

            if (Input.GetMouseButtonDown(0) && progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }

    }
}
