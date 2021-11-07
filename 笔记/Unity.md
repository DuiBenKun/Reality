# Unity

	##  	地图绘制篇	

  *   地图绘制是要注意素材的像素单位

  *   素材引入Tile Palette时，注意分割（选中素材—>调整像素单位—>点击Sprite Editor—>开始切割）

  * Tile Palette是绘制地图过程中最常用的tab

<<<<<<< Updated upstream
    ## 角色移动篇（2D）
=======
    ### 角色移动篇（2D）[^1]
>>>>>>> Stashed changes

    [^1]:（**注：按键设置在Edit—>Project Settings—>Input里。**）

* 角色移动输入可以调用`Input.GetAxis("Horizontal")`方法来实现键盘监听

* 真正让角色移动起来可以通过一下方法实现：

  * 调用刚体并利用`Vector2`给角色施加一个力使其移动：

    ~~~c#
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

<<<<<<< Updated upstream
    
    
    ## 镜头控制篇

* 高级虚拟摄像头    **（PS：使用Cinemachine时，你需要记住一个准则：*场景中一般只有一个Unity相机（除了需要制作分屏、画中画等效果的时候），上面挂着CinemachineBrain脚本；其他相机都是使用Cinemachine中的虚拟相机，可以有很多个。*）**

  * 各种参数的说明：

    * **Status:Live**：用于调试；选中该按钮时，次虚拟相机会直接控制Unity相机显示在Game窗口中，用于相机的调试。*（忽略优先级，但是需要所在GameOjbect是激活状态）*

    * **Game Window Guides**：勾选时，Game窗口会显示辅助线，用于设置虚拟相机的各个属性。

      需要注意的是，仅在以下任一属性赋值时显示辅助线：
=======
>>>>>>> Stashed changes

      ~~~
      Look At属性设置了物体，Aim设置为Composer或Group Composer
      Follow属性设置了物体，Body设置为Framing Composer
      ~~~

<<<<<<< Updated upstream
    * **Save During Play**：虚拟相机的属性在运行时的修改可以被保存下来，退出Play状态时不会被重置。
    * **Priority**：优先级——虚拟相机的重要程度，用于Live镜头的选择。数值越高代表优先级越高。Cinemachine Brain会根据这个属性从所有激活的虚拟相机中选择活动的虚拟相机。在Timeline上使用时这个属性不生效。
=======
  ### 镜头控制篇

*   高级虚拟相机的参数介绍：[^Photo]

   * **Status:Live/Standby/Disable**——当前该虚拟相机的状态：Live——正在使用；Standby——准备使用；Disable——不可使用。*（sole表示单独调用该相机）*

   * **Game Window Guides**——游戏窗口指示器：在游戏窗口中展现镜头设置的指示器。

   * **Save During Play**——在试玩时保存调整。

   * **Priority**——优先级：用于多镜头时设置镜头优先级。

   * **Follow**——跟随目标：镜头跟踪的目标。

   * **Look At**——视角目标：控制镜头视角；用于旋转镜头。

   * **Standby Update**

   * **Lens**——镜头常用配置：

     * Orthographic Size——视角区域；
     * Near Clip Plane——最近裁切平面；
     * Far Clip Plane——最远裁切平面；
     * Dutch——镜头旋转速度。

   * **Transitions**

     * *Blend Hint*——混合模式

       — None 无，默认线性混合

       — Spherical Position 根据Look At的物体球面旋转混合

       — Cylindrical Position 根据Look At的物体柱面旋转混合（水平方向圆弧，垂直方向线性）

       — Screen Space Aim When Target 在屏幕空间瞄准目标

     * *Inherit Positon* 下一个相机变成活动相机时，从上一个相机继承位置，即保持两个相机位置相同。

     * *On Camera Live*事件，相机变为活动时会触发对应的事件。

   * **Body**

     Body属性由下列算法控制移动相机：

     ​	— Do Nothing：不移动虚拟相机
     ​	— Framing Transposer：在屏幕空间，保持相机和跟随目标的相对位置，可以设置缓动。
     ​	— Hard Lock to Target：虚拟相机和跟随目标使用相同位置。
     ​	— Orbital Transposer：相机和跟随目标的相对位置是可变的，还能接收用户的输入。常见于玩家控制		的相机。
     ​	— Tracked Dolly：相机沿着预先设置的轨道移动。
     ​	— Transposer：相机和跟随目标的相对位置固定，可以设置缓动。

     [Body类型]

     ![Body类型](C:\Users\75759\Desktop\unity\Fox\笔记\Body类型.jpg)

     [Body各属性详解](https://www.bilibili.com/read/cv4702573?spm_id_from=333.999.0.0)

     

   * **Aim**

     ![Aim类型](C:\Users\75759\Desktop\unity\Fox\笔记\Aim.png)

     [Aim各属性详解](https://www.bilibili.com/read/cv4789100?from=search)

     

   * **Noise**——模拟抖动

     [Noise详解](https://www.bilibili.com/read/cv4902679?spm_id_from=333.999.0.0)

     

     

     [^Photo]:![高级虚拟相机的参数](C:\Users\75759\Desktop\unity\Fox\笔记\高级虚拟相机的参数.png)

>>>>>>> Stashed changes

    * **Follow**：跟踪对象——虚拟相机会跟随这个物体移动。**Body**属性基于这个目标物体更新Unity相机的位置。

    * **Look At**：注视对象——镜头瞄准的物体目标。**Aim**属性使用这个属性来更新Unity相机的旋转。

    * **Standby Update**：待命时的更新方式，当虚拟相机物体没有被禁用，但是优先级不足时，虚拟相机处于待命状态。这个属性会影响性能，通常设置为Never是最好的，但是有时候可能需要虚拟相机更新来做一些镜头相关的计算判断。

          Never 不更新
          Always 每帧更新
          Round Robin 循环更新：所有的待命虚拟相机循环更新，每帧更新一个相机（例如有10个待命的相机，第一帧更新第一个相机，第2帧更新第2个相机，…，第11帧更新第1个相机，以此类推）

    * **Lens** ：镜头常用配置
      1. Orthographic Size：视角区域；
      2. Near/Far Clip Plane：最近最远裁切平面；
      3. Dutch：镜头旋转角度。
    * **Transitions**：
    * **Body**：[Cinemachine智能相机Body属性讲解](https://www.bilibili.com/read/cv4702573/)

    * **Aim**：瞄准设置——有三种设置：*Hard constraint*：固定距离，没有任何平滑效果。*Composer*：有平滑效果的瞄准目标。*Group Composer*：同时瞄准多个目标。
    * **Noise**：噪声设置——模拟手持相机抖动效果

![高级虚拟摄像头的参数](C:\Users\75759\Desktop\unity\Fox\笔记\高级虚拟摄像头的基本参数.png)



# Code

<<<<<<< Updated upstream
### 移动类

* Transform是类，而transform是指当前对象的transform参数；



### 碰撞类

* OnCollisionEnter2D(Collision2D collision)碰撞进入则调用

  OnCollisionExit2D(Collision2D collision)碰撞退出则调用



## 细节：

[FixedUpdate 与 Update的区别](https://www.cnblogs.com/shirln/p/8471909.html#update%E5%92%8Cfixedupdate%E5%8C%BA%E5%88%AB)

=======
* **Update**、**FixedUpdate**以及**LateUpdate**的区别：

  ​	—— **Update**跟当前平台的帧数有关，在每次渲染新的一帧时才会调用。

  ​	—— **FixedUpdate**是真实时间，不受游戏帧率影响；所以一般出理物理逻辑时使用FixedUpdate。

  ​	—— **LateUpdate**是在所有**Update**函数被调用后再调用的；能起到调整脚本执行顺序的作用。

  ​	*（PS：FixedUpdate的时间间隔可以在项目设置中更改，Edit->ProjectSetting->time 找到Fixedtimestep。就可以修改了。）*

* **Transform**是类，**transform**是指当前对象的**transform**参数；
>>>>>>> Stashed changes
