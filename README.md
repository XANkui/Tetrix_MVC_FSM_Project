# Tetrix_MVC_FSM_Project
使用 MVC 和 FSM，实现俄罗斯方块

内容说明：

1、使用 MVC 思想，构建工程（其中 M 为 俄罗斯方块的 map 数据管理； V 为 UI 界面管理； C 相关的逻辑控制，包括，GameManager，AudioManager，CameraManager, Shape 的操作， 等等）；

2、使用有限状态机 FSM，控制 MenuState 状态和 PlayState 状态；

3、使用 DoTween Pro 实现 UI 的动画效果；

4、整个游戏包括：1）开始按钮，重新开始按钮，设置按钮，历史数据按钮

               2）设置按钮，可以控制声音，链接网络等
                
               3）历史数据按钮，可以查看历史的游戏最高分，游戏次数，游戏数据也可以清除
               
               4）游戏界面有当前分数，最高分，游戏暂停
               
               5）游戏操作：左右方向键，实现移动 Shape；向上方向键，旋转 Shape； 向下方向键，加速下落 Shape
