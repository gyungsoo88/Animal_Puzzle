using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource supermode_sound;
    public static GameManager instance;
    Animator anim;
    AudioSource fxSound;
    public GameObject effect1;
    public GameObject effect2;
    public string nextSceneName;
    public AudioClip backMusic;
    public AudioClip score_sound;
    public AudioClip score_sound2;
    public AudioClip score_sound3;
    int score_sound_index=0;
 

    public AudioClip cat_sound;
    public AudioClip sheep_sound;
    public AudioClip ending_sound;
    public AudioClip duck_sound;
    public AudioClip penguin_sound;
    public AudioClip potion_sound;
    public TextMeshPro score_text;
    public TextMeshPro top_score_text;
    public TextMeshPro Effect_text;
    public TextMeshPro arrow1;
    public TextMeshPro arrow2;
    public float textloop = 0f;
    
    int score_number = 0;

    public GameObject[] pieces = new GameObject[6];
    public GameObject tile1;
    public GameObject tile2;

    public GameObject[,] gametiles = new GameObject[6, 6];

    GameObject upgrade;
    GameObject on_hand;
    GameObject walker;


    float supermode_loop = 0;
    int number;
    bool check = true;
    int highestScore;
    bool check2 = true;
    bool check3 = true;
    bool check4 = true;
    bool explosion = false;
    bool supermode = false;
    bool supermode_activation = false;
    bool ender = false;
    float explosion_loop = 0;
    int score = 0;
    int a, b;
    int loop = 0;
    void search2(int i, int j)
    {
        if ((i < 6) && (j < 6) && (i >= 0) && (j >= 0))
        {
            if(supermode == true)
            {

                score = 5;
                for(i=0;i<6;++i)
                {
                    for(j=0;j<6;++j)
                    {
                        if (gametiles[i,j].tag==on_hand.tag)
                        {
                            gametiles[i, j].GetComponent<search_check>().d_check = true;
                            gametiles[i, j].GetComponent<search_check>().s_check = true;
                        }
                    }
                }
                return;
            }
            if (on_hand == null)
            { return; }
            GameObject obj2 = on_hand;
            if (obj2.tag == "tile0")
                return;
            obj2.GetComponent<search_check>().d_check = true;
            obj2.GetComponent<search_check>().s_check = true;

            if (i < 5)
            {
                obj2 = gametiles[i + 1, j];
                if (obj2.GetComponent<search_check>().s_check == false)
                {
                    if (obj2.tag == on_hand.tag)
                    {
                        search(i + 1, j);
                        obj2.GetComponent<search_check>().d_check = true;
                        ++score;
                    }
                    else
                    {
                        obj2.GetComponent<search_check>().s_check = true;
                    }
                }
            }
            if (j < 5)
            {
                obj2 = gametiles[i, j + 1];
                if (obj2.GetComponent<search_check>().s_check == false)
                {
                    if (obj2.tag == on_hand.tag)
                    {
                        search(i, j + 1);
                        ++score;
                        obj2.GetComponent<search_check>().d_check = true;

                    }
                    else
                    {
                        obj2.GetComponent<search_check>().s_check = true;
                    }
                }
            }
            if (i > 0)
            {
                obj2 = gametiles[i - 1, j];
                if (obj2.GetComponent<search_check>().s_check == false)
                {
                    if (obj2.tag == on_hand.tag)
                    {
                        search(i - 1, j);
                        ++score;
                        obj2.GetComponent<search_check>().d_check = true;

                    }
                    else
                    {
                        obj2.GetComponent<search_check>().s_check = true;
                    }
                }
            }
            if (j > 0)
            {
                obj2 = gametiles[i, j - 1];
                if (obj2.GetComponent<search_check>().s_check == false)
                {
                    if (obj2.tag == on_hand.tag)
                    {
                        search(i, j - 1);
                        ++score;
                        obj2.GetComponent<search_check>().d_check = true;

                    }
                    else
                    {
                        obj2.GetComponent<search_check>().s_check = true;
                    }
                }
            }
            return;
        }


    }
    void potion_search()
    {
        
        for (int i = 0; i < 6; ++i)
        {
            for (int j = 0; j < 6; ++j)
            {
                if ((gametiles[i, j].GetComponent<search_check>().r_check == true) && (gametiles[i, j].GetComponent<search_check>().d_check == true))
                {
                    fxSound.PlayOneShot(potion_sound, 3f);
                    for (int k = 0; k < 6; ++k)
                    {
                        gametiles[k, j].GetComponent<search_check>().d_check = true;
                        gametiles[k, j].GetComponent<search_check>().s_check = true;


                        gametiles[i, k].GetComponent<search_check>().d_check = true;
                        gametiles[i, k].GetComponent<search_check>().s_check = true;
                    }
                }
                else if ((gametiles[i, j].GetComponent<search_check>().b_check == true) && (gametiles[i, j].GetComponent<search_check>().d_check == true))
                {
                    fxSound.PlayOneShot(potion_sound, 3f);
                    int a = i;
                    int b = j;
                    while ((a != 0) && (b != 0))
                    {
                        --a;--b;
                    }
                    while((a!=6)&&(b!=6))
                    {
                        gametiles[a, b].GetComponent<search_check>().d_check = true;
                        gametiles[a, b].GetComponent<search_check>().s_check = true;
                        ++a; ++b;
                    }
                    a = i;
                    b = j;
                    while ((a != 5) && (b != 0))
                    {
                        ++a; --b;
                    }
                    while ((a != -1) && (b != 6))
                    {
                        gametiles[a, b].GetComponent<search_check>().d_check = true;
                        gametiles[a, b].GetComponent<search_check>().s_check = true;
                        --a; ++b;
                    }
                    
                }
            }
        }
    }
    void search(int i, int j)
    {
        if ((i < 6) && (j < 6) && (i >= 0) && (j >= 0))
        {
            if (supermode_activation == true)
            {

                score = 5;
                for (i = 0; i < 6; ++i)
                {
                    for (j = 0; j < 6; ++j)
                    {
                        if (gametiles[i, j].tag == on_hand.tag)
                        {
                            gametiles[i, j].GetComponent<search_check>().d_check = true;
                            gametiles[i, j].GetComponent<search_check>().s_check = true;
                        }
                    }
                }
                
                return;
            }
           

            GameObject obj = gametiles[i, j];
            if (obj.tag == "tile0")
                return;
            obj.GetComponent<search_check>().d_check = true;
            obj.GetComponent<search_check>().s_check = true;

            if (i < 5)
            {
                GameObject obj2 = gametiles[i + 1, j];
                if (obj2.GetComponent<search_check>().s_check == false)
                {
                    if (obj2.tag == obj.tag)
                    {
                        search(i + 1, j);
                        obj2.GetComponent<search_check>().d_check = true;
                        ++score;
                    }
                    else
                    {
                        obj2.GetComponent<search_check>().s_check = true;
                    }
                }
            }
            if (j < 5)
            {
                GameObject obj2 = gametiles[i, j + 1];
                if (obj2.GetComponent<search_check>().s_check == false)
                {
                    if (obj2.tag == obj.tag)
                    {
                        search(i, j + 1);
                 
                            ++score;
                      
                        obj2.GetComponent<search_check>().d_check = true;

                    }
                    else
                    {
                        obj2.GetComponent<search_check>().s_check = true;
                    }
                }
            }
            if (i > 0)
            {
                GameObject obj2 = gametiles[i - 1, j];
                if (obj2.GetComponent<search_check>().s_check == false)
                {
                    if (obj2.tag == obj.tag)
                    {
                        search(i - 1, j);
                  
                            ++score;
                
                        obj2.GetComponent<search_check>().d_check = true;

                    }
                    else
                    {
                        obj2.GetComponent<search_check>().s_check = true;
                    }
                }
            }
            if (j > 0)
            {
                GameObject obj2 = gametiles[i, j - 1];
                if (obj2.GetComponent<search_check>().s_check == false)
                {
                    if (obj2.tag == obj.tag)
                    {
                        search(i, j - 1);
            
                            ++score;
               
                        obj2.GetComponent<search_check>().d_check = true;

                    }
                    else
                    {
                        obj2.GetComponent<search_check>().s_check = true;
                    }
                }
            }

            return;
        }



    }
    void search_reset()
    {
        int i, j;
        for (i = 0; i < 6; ++i)
        {
            for (j = 0; j < 6; ++j)
            {
                GameObject obj = gametiles[i, j];
                obj.GetComponent<search_check>().s_check = false;
                obj.GetComponent<search_check>().d_check = false;
            }
        }

    }
    void game_end()
    {
        int i, j;

        for (i = 0; i < 6; ++i)
        {
            for (j = 0; j < 6; ++j)
            {
                if (gametiles[i, j].tag == "tile0")
                {
             
                    return;
                }

            }
        }
        ender = true;
        
    }
    void destroy()
    {

        int i, j;

        for (i = 0; i < 6; ++i)
        {
            for (j = 0; j < 6; ++j)
            {
                GameObject obj = gametiles[i, j];
                if ((obj.GetComponent<search_check>().s_check == true) && (obj.GetComponent<search_check>().d_check == true))
                {
                    Destroy(obj);

                    gametiles[i, j] = Instantiate(pieces[0], new Vector3(i, 0, j), Quaternion.identity);
                    Instantiate(effect1, new Vector3(i, 1, j), Quaternion.identity);
                    ++score_number;



                }


                obj.GetComponent<search_check>().s_check = false;

            }
        }
        score_text.text = "" + score_number;

    }
    void rotate()
    {
        int i, j;
        for (i = 0; i < 6; ++i)
        {
            for (j = 0; j < 6; ++j)
            {
                GameObject obj = gametiles[i, j];
                if ((obj.GetComponent<search_check>().s_check == true) && (obj.GetComponent<search_check>().d_check == true))
                {

                    if (obj.tag != "tile0")
                    {
                        anim = obj.GetComponent<Animator>();
                        if (obj.tag == "tile3")
                        {
                            obj.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 10000 * Time.deltaTime, 0);
                        }
                        else
                        {
                            anim.SetBool("stun", true);
                        }

                    }
                }

                obj.GetComponent<search_check>().s_check = false;

            }
        }


    }
    bool search_destroy(int i, int j)
    {
        score = 0;
        search_reset();
        search(i, j);
       
        if (score > 1)
        {
            potion_search();
            destroy();
            search_reset();
            if ((supermode_activation) && supermode)
            {
                supermode_activation = false;
                supermode = false;
            }
            score = 0;
            return true;
            
        }
        search_reset();
        score = 0;
     
        return false;
    }


    void move_animals()
    {
        for (int j = 4; j >= 0; --j)
        {
            for (int i = 5; i >= 0; --i)
            {
                if ((gametiles[i, j + 1].tag == "tile0") && (gametiles[i, j].tag != "tile0"))
                {

                    Destroy(gametiles[i, j + 1]);
                    gametiles[i, j + 1] = Instantiate(gametiles[i, j], new Vector3(i, 0, j + 1), Quaternion.identity);
                    Destroy(gametiles[i, j]);
                    gametiles[i, j] = Instantiate(pieces[0], new Vector3(i, 0, j), Quaternion.identity);
                }

            }
        }
        for (int i = 0; i < 6; ++i)
        {
            if (gametiles[i, 0].tag == "tile0")
            {
                Destroy(gametiles[i, 0]);
                
                if (loop>40)
                    gametiles[i, 0] = Instantiate(pieces[Random.Range(1, 7)], new Vector3(i, 0, 0), Quaternion.identity);
                else if (loop < 20)
                    gametiles[i, 0] = Instantiate(pieces[Random.Range(1, 5)], new Vector3(i, 0, 0), Quaternion.identity);
                else
                    gametiles[i, 0] = Instantiate(pieces[Random.Range(1, 6)], new Vector3(i, 0, 0), Quaternion.identity);
                int t = Random.Range(0, 20);
                if (t == 5)
                {
                    gametiles[i, 0].GetComponent<search_check>().r_check = true;
                }
                else if (t == 4)
                {
                    gametiles[i, 0].GetComponent<search_check>().b_check = true;
                }

            }
        }
        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                search(i, j);

                if (score > 1)
                {
                    explosion = true;
                }
            }

        }
        if (highestScore > score_number)
            top_score_text.text = "" + highestScore;
        else
            top_score_text.text = "" + score_number;

        if (!explosion)
            game_end();

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject); 
    }

    void Start()
    {
        fxSound = GetComponent<AudioSource>();
        fxSound.Play();
    
 
        highestScore = PlayerPrefs.GetInt("Top_score", 0);
        top_score_text.text = "" + highestScore;




        int k;
        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                if (check2)
                {
                    Instantiate(tile1, new Vector3(i, -1, j), Quaternion.identity);
                    check2 = false;
                    if (i == 5)
                    {
                        check2 = true;
                    }
                }
                else
                {
                    Instantiate(tile2, new Vector3(i, -1, j), Quaternion.identity);
                    check2 = true;
                    if (i == 5)
                    {
                        check2 = false;
                    }
                }

                k = Random.Range(0, 10);
                if (k < 5)
                {
                    k = 0;
                }
                else
                {
                    k = k - 5;
                }
                gametiles[i, j] = Instantiate(pieces[k], new Vector3(i, 0, j), Quaternion.identity);
            }
        }
        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                search_destroy(i, j);
            }

        }
        search_reset();
        score = 0;
        score_number = 0;
        score_text.text = "" + score_number;
    }


    void Update()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 dir;

        if (Physics.Raycast(ray, out hit))
        {
            if (check == true)
            {
               
                number = Random.Range(1, 5);
                if(Random.Range(1, 20)==5)
                {
                    supermode = true;
                    supermode_sound.PlayOneShot(supermode_sound.clip, 0.5f);
                }

                on_hand = Instantiate(pieces[number], hit.point, Quaternion.identity);
                on_hand.GetComponentInChildren<MeshCollider>().enabled = !on_hand.GetComponentInChildren<MeshCollider>().enabled;
                on_hand.GetComponent<Rigidbody>().useGravity = false;
              check = false;
            }
        }
        int i = Mathf.RoundToInt(hit.point.x);
        int j = Mathf.RoundToInt(hit.point.z);
       
        if (on_hand != null)
        {
            dir = (hit.point - on_hand.transform.position);
            dir.y = 0;
            on_hand.transform.Translate(10 * dir * Time.deltaTime, Space.World);
        }



        if ((Input.GetMouseButtonDown(0)) && (check3 == true)&&(explosion==false))
        {
            if ((0 <= i) && (i <= 5) && (j >= 0) && (j <= 5) && (gametiles[i, j].tag == "tile0"))
            {
                on_hand.GetComponentInChildren<MeshCollider>().enabled = !on_hand.GetComponentInChildren<MeshCollider>().enabled;
                on_hand.GetComponent<Rigidbody>().useGravity = true;

                gametiles[i, j] = Instantiate(on_hand, new Vector3(i, 0, j), Quaternion.identity);
                if (on_hand.tag == "tile3")
                {
                    fxSound.PlayOneShot(cat_sound, 0.7f);
                }
                if (on_hand.tag == "tile2")
                {
                    fxSound.PlayOneShot(duck_sound, 2.8f);
                }
                if (on_hand.tag == "tile1")
                {
                    fxSound.PlayOneShot(sheep_sound, 2.8f);
                    
                }
                if (on_hand.tag == "tile4")
                {
                    fxSound.PlayOneShot(penguin_sound, 2.8f);
                }
                Destroy(on_hand);
                if (supermode)
                {
                    supermode_activation = true;
                }
                check = true;
                check3 = false;
            }
        }
        else
        {
            if ((0 <= i) && (i <= 5) && (j >= 0) && (j <= 5) && (gametiles[i, j].tag == "tile0"))
            {
                search_reset();
                score = 0;
                search2(i, j);
                if (score > 1)
                {
                    rotate();
                 
                }
                search_reset();
                score = 0;
            }
        }
        if (check3 == false)
        {
            
            search_reset();
            score = 0;
            search(i, j);
            if (score > 1)
            {
                
                search_destroy(i, j);
               
                supermode_sound.Stop();

                if (score_sound_index == 0)
                    fxSound.PlayOneShot(score_sound, 1f);
                else if (score_sound_index == 1)
                    fxSound.PlayOneShot(score_sound2, 1f);
                else if (score_sound_index == 2)
                    fxSound.PlayOneShot(score_sound3, 1f);
                else if (score_sound_index == 3)
                    fxSound.PlayOneShot(score_sound3, 1f);
                else if (score_sound_index == 4)
                    fxSound.PlayOneShot(score_sound, 1f);
                score_sound_index = (score_sound_index + 1) % 5;

            }
            check3 = true;
            move_animals();   
            ++loop;
            search_reset();
            score = 0;

        }
        if (explosion ==true)
        {
            explosion_loop += Time.deltaTime;
            if (explosion_loop > 1)
            {
                explosion_loop = 0;
                for (int b = 0; b < 6; b++)
                {
                    for (int a = 0; a < 6; a++)
                    {

                        bool another_check = search_destroy(a, b);
                        if (another_check)
                        {
                            if (score_sound_index == 0)
                                fxSound.PlayOneShot(score_sound, 1f);
                            else if (score_sound_index == 1)
                                fxSound.PlayOneShot(score_sound2, 1f);
                            else if (score_sound_index == 2)
                                fxSound.PlayOneShot(score_sound3, 1f);
                            else if (score_sound_index == 3)
                                fxSound.PlayOneShot(score_sound3, 1f);
                            else if (score_sound_index == 4)
                                fxSound.PlayOneShot(score_sound, 1f);
                            score_sound_index = (score_sound_index + 1) % 5;

                            return;
                        }
                    }
                }
                explosion = false;
                
                    game_end();

            }
            
        }

        if (ender==true)
        {
            if (check4 == true)
            {
                fxSound.Stop();
                fxSound.PlayOneShot(ending_sound, 0.7f);
                check4 = false;
            }
            Effect_text.GetComponent<MeshRenderer>().enabled = true;
            if (score_number > highestScore)
            {
                PlayerPrefs.SetInt("Top_score", score_number);
            }
            if (Input.GetButtonDown("Submit"))
                SceneManager.LoadScene(nextSceneName);
           

        }

        textloop += Time.deltaTime;
        if ((textloop >= 0) && (textloop < 1))
        {
            arrow1.text = ">";
            arrow2.text = ">";
        }
        else if ((textloop >= 1) && (textloop < 2))
        {
            arrow1.text = ">>";
            arrow2.text = ">>";
        }
        else if ((textloop >= 2) && (textloop < 3))
        {
            arrow1.text = ">>>";
            arrow2.text = ">>>";
        }
        else if ((textloop >= 3) && (textloop < 4))
        {
            arrow1.text = ">>>>";
            arrow2.text = ">>>>";
        }
        else
            textloop = 0;

        if (supermode == true)
        {
            if (supermode_loop>1)
            {
                Instantiate(effect2, new Vector3(i,0,j), Quaternion.identity);
                supermode_loop = 0;
            }
            supermode_loop+=Time.deltaTime; 
        }
    }
}
