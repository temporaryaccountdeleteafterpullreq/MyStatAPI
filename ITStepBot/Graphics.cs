﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ITStepBot
{
    static class Graphics
    {
        public static void ShowHello()
        {
            Console.Write(@"
      ___           ___           ___                                                ___                 
     /  /\         /  /\         /  /\                                _____         /  /\          ___   
    /  /:/        /  /::\       /  /::\                              /  /::\       /  /::\        /  /\  
   /  /:/        /  /:/\:\     /  /:/\:\    ___     ___             /  /:/\:\     /  /:/\:\      /  /:/  
  /  /:/  ___   /  /:/  \:\   /  /:/~/::\  /__/\   /  /\           /  /:/~/::\   /  /:/  \:\    /  /:/   
 /__/:/  /  /\ /__/:/ \__\:\ /__/:/ /:/\:\ \  \:\ /  /:/          /__/:/ /:/\:| /__/:/ \__\:\  /  /::\   
 \  \:\ /  /:/ \  \:\ /  /:/ \  \:\/:/__\/  \  \:\  /:/           \  \:\/:/~/:/ \  \:\ /  /:/ /__/:/\:\  
  \  \:\  /:/   \  \:\  /:/   \  \::/        \  \:\/:/             \  \::/ /:/   \  \:\  /:/  \__\/  \:\ 
   \  \:\/:/     \  \:\/:/     \  \:\         \  \::/               \  \:\/:/     \  \:\/:/        \  \:\
    \  \::/       \  \::/       \  \:\         \__\/                 \  \::/       \  \::/          \__\/
     \__\/         \__\/         \__\/                                \__\/         \__\/                
            ");
            Console.Write($"{Environment.NewLine} {Environment.NewLine}");
        }
    }
}
