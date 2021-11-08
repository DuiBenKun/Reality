# Unity

### 					地图绘制篇	

  *   地图绘制是要注意素材的像素单位

  *   素材引入Tile Palette时，注意分割（选中素材—>调整像素单位—>点击Sprite Editor—>开始切割）

  * Tile Palette是绘制地图过程中最常用的tab

    ### 角色移动篇（2D）

    （**注：按键设置在Edit—>Project Settings—>Input里。**）

* 角色移动输入可以调用`Input.GetAxis("Horizontal")`方法来实现键盘监听

* 真正让角色移动起来可以通过一下方法实现：

  * 调用刚体并利用vector2给角色施加一个力使其移动：

    ~~~c#
    //以下是控制水平移动
    float HorizontalMove = Input.GetAxis("Horizontal");
    GameObjectRigidbody2D.velocity = new Vector2(HorizontalMove * speed * Time.deltaTime, GameObjectRigidbody2D.velocity.y);
    //y轴上没有速度则y轴上角色不动
    //Time.deltaTime是为了让游戏在所有机器中保持一致的运行速度而加的；表示一个物理时钟的时间变化量
    ~~~

    

  * 通过改变`Transform`里的`Position`来实现：利用方法

    ```c#
    float HorizontalMove = Input.GetAxis("Horizontal");
    transform.Translate(transform.right * Time.deltaTime * HorizontalMove*speed);
    //注意加上speed，不然就是一帧动一点）
    ```

    

    ***注意：speed和velocity均表示速度；但是velocity是一个矢量，而speed只是一个常数。所以如果用speed来移动要额外指明方向。***

    **Ps：在Animator中，游戏一开始就会进入idle状态，所以进入run状态的条件可以只是 *Conditions—>run—>true*  +  `SetBool("run", true);`**

    

* 角色在移动时面朝方向问题

   * 可以通过`Input.GetAxis`实现：

     ~~~ c#
     float HorizontalMove = Input.GetAxis("Horizontal");
      if (HorizontalMove == 1)             //h=1则右移动
                 {
                     GetComponent< SpriteRenderer > ().flipX = false;            
          			//角色正向
                 }
                 if(HorizontalMove == -1)
                 {
                     GetComponent< SpriteRenderer > ().flipX = true;             
                     //角色反向
                 }
     ~~~

     （*但是这种方法存在这一定的转向延迟。因为利用的是`Input.GetAxis`，则`HorizontalMove`的变化是*

     一个平滑过程，所以存在一定的延迟转向。*）

  * 通过`Input.GetAxisRaw`实现：

    ~~~c#
    float FaceDircetion = Input.GetAxisRaw("Horizontal");
    if(FaceDircetion != 0)
    {
        transform.localScale = new Vector3(FaceDircetion, 1, 1);
    }
    ~~~

    

  







# Code

* Transform是类，transform是指当前对象的transform参数；
